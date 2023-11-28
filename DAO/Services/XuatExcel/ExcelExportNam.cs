using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Charts;
using DAO.ViewModel;

namespace DAO.Services.XuatExcel
{
    public class ExcelExportNam

    {

        public static string BaoCaoNam(int[] Id_ChiNhanh, int[] Id_Tram, int[] Id_ThongSo, DateTime StartDate, DateTime EndDate, int CbThoiGian)
        {

            // var data = DAO.Services.DanhMuc.NhatKyNamService.Get_prc_Nhat_Ky_Nam("", "", "");
            var resultModel = DAO.Services.DanhMuc.NhatKyNamService.Get_prc_Nhat_Ky_Nam(string.Join(',', Id_ChiNhanh), string.Join(',', Id_Tram), string.Join(',', Id_ThongSo), StartDate.Date, EndDate.Date);
            var FileName = "Báo Cáo Năm.xlsx";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\file");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch { }
            }

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\file", FileName);

            if (resultModel == null || resultModel.Data == null)
            {
                // Handle the case where data retrieval is unsuccessful
                // You can add error handling or return an error message here.
                return "Error: Unable to retrieve data";
            }

            List<prc_Nhat_Ky_Nam> ListNhatKyNam = resultModel.Data;
            var data = ListNhatKyNam.GroupBy(x => x.Thoi_Gian).OrderByDescending(group => group.Key);
            var latestThoiGian = data.OrderByDescending(item => item.Key).FirstOrDefault();
            switch (CbThoiGian)
            {
                case 1:
                    data = ListNhatKyNam.GroupBy(x => x.Thoi_Gian).OrderByDescending(group => group.Key);
                    break;
                case 2:
                    data = ListNhatKyNam
                        .Where(x => x.Thoi_Gian.Minute == 0)
                        .GroupBy(x => x.Thoi_Gian)
                        .OrderByDescending(group => group.Key);
                    break;
                case 3:



                    data = ListNhatKyNam
                        .Where(x => x.Thoi_Gian == latestThoiGian.Key)
                        .GroupBy(x => x.Thoi_Gian)
                        .OrderByDescending(group => group.Key);
                    break;
                    break;

                default:
                    // Handle an invalid CbThoiGian value or set a default behavior.
                    data = ListNhatKyNam.GroupBy(x => x.Thoi_Gian).OrderByDescending(group => group.Key);
                    break;
            }


            var dataFirst = data.FirstOrDefault();

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(FilePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylePart.Stylesheet = GenerateStylesheet();
                stylePart.Stylesheet.Save();
                Columns columns = new Columns(
         new Column // STT column
         {
             Min = 1,
             Max = 2,
             Width = 5,
             CustomWidth = true
         },
         new Column // Tên cuộc họp
         {
             Min = 2,
             Max = 3,
             Width = 30,
             CustomWidth = true
         },
          new Column // Tên cuộc họp
          {
              Min = 3,
              Max = Convert.ToUInt32(dataFirst.Count()),
              Width = 12,
              CustomWidth = true
          }

        );

                worksheetPart.Worksheet.AppendChild(columns);
                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                MergeCells mergeCells = new MergeCells();
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:A2") });



                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Sheet1"
                };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);

                var ListText = ("A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z").Split(";").ToList();
                var ListRows = new List<RowExcells>();
                var dem = 1;
                foreach (var a in ListText)
                {
                    var RowData = new RowExcells();
                    RowData.id = dem;
                    RowData.ten_cot = a;
                    ListRows.Add(RowData);
                    dem++;
                }
                foreach (var a in ListText)
                {
                    foreach (var b in ListText)
                    {
                        var RowData = new RowExcells();
                        RowData.id = dem;
                        RowData.ten_cot = a + b;
                        ListRows.Add(RowData);
                        dem++;
                    }
                }
                foreach (var a in ListText)
                {
                    foreach (var b in ListText)
                    {
                        foreach (var c in ListText)
                        {
                            var RowData = new RowExcells();
                            RowData.id = dem;
                            RowData.ten_cot = a + b + c;
                            ListRows.Add(RowData);
                            dem++;
                        }
                    }

                }

                // Construct the header row with merged cells
                Row row = new Row();
                Row row1 = new Row();
                row.Append(ConstructCell("STT", CellValues.String));
                row.Append(ConstructCell("Thời Gian", CellValues.String));
                row1.Append(ConstructCell("STT", CellValues.String));
                row1.Append(ConstructCell("Thời Gian", CellValues.String));
                mergeCells.Append(new MergeCell() { Reference = new StringValue("B1:B2") });
                var check = 2;
                foreach (var items in dataFirst.GroupBy(x => x.TenTram))
                {
                    foreach (var item in items.OrderBy(x => x.TenThongSo))
                    {
                        row.Append(ConstructCell(items.Key, CellValues.String, 1));
                        row1.Append(ConstructCell(item.TenThongSo, CellValues.String, 1));
                        if (item.TenThongSo == "TỔNG LƯU LƯỢNG 1" || item.TenThongSo == "TỔNG LƯU LƯỢNG 2" || item.TenThongSo == "TỔNG LƯU LƯỢNG 3"
                                    || item.TenThongSo == "TỔNG LƯU LƯỢNG 4" || item.TenThongSo == "TỔNG LƯU LƯỢNG 5" || item.TenThongSo == "ÁP LỰC 2")
                        {
                            row.Append(ConstructCell(items.Key, CellValues.String, 1));
                            row1.Append(ConstructCell("TIÊU THỤ", CellValues.String, 1));
                        }
                    }
                    var demTieuThu = 0;
                    if (items.FirstOrDefault(x => x.TenThongSo == "TỔNG LƯU LƯỢNG 1") != null)
                    {
                        demTieuThu++;
                    }
                    if (items.FirstOrDefault(x => x.TenThongSo == "TỔNG LƯU LƯỢNG 2") != null)
                    {
                        demTieuThu++;
                    }
                    if (items.FirstOrDefault(x => x.TenThongSo == "TỔNG LƯU LƯỢNG 3") != null)
                    {
                        demTieuThu++;
                    }
                    if (items.FirstOrDefault(x => x.TenThongSo == "TỔNG LƯU LƯỢNG 4") != null)
                    {
                        demTieuThu++;
                    }
                    if (items.FirstOrDefault(x => x.TenThongSo == "TỔNG LƯU LƯỢNG 5") != null)
                    {
                        demTieuThu++;
                    }
                    if (items.FirstOrDefault(x => x.TenThongSo == "ÁP LỰC 2") != null)
                    {
                        demTieuThu++;
                    }

                    var bien1 = check + 1;
                    var cot1 = ListRows.FirstOrDefault(x => x.id == bien1).ten_cot;
                    var bien2 = check + demTieuThu + items.Count();
                    var cot2 = ListRows.FirstOrDefault(x => x.id == bien2).ten_cot;
                    mergeCells.Append(new MergeCell() { Reference = new StringValue(cot1 + "1:" + cot2 + "1") });
                    check = check + demTieuThu + items.Count();
                }
                sheetData.AppendChild(row);
                sheetData.AppendChild(row1);
                var i = 1;
                foreach (var list in data)
                {
                    row = new Row();
                    row.Append(ConstructCell(i.ToString(), CellValues.String, 1));
                    row.Append(ConstructCell(list.Key.ToString("dd/MM/yyyy HH:mm:ss"), CellValues.String, 1));
                    foreach (var items in list.GroupBy(x => x.TenTram))
                    {
                        var j = 0;
                        foreach (var item in items.OrderBy(x => x.TenThongSo))
                        {
                            var gt = item.Gia_Tri != null ? item.Gia_Tri.ToString() : "";
                            row.Append(ConstructCell(gt, CellValues.String, 1));
                            if (item.TenThongSo == "TỔNG LƯU LƯỢNG 1" || item.TenThongSo == "TỔNG LƯU LƯỢNG 2" || item.TenThongSo == "TỔNG LƯU LƯỢNG 3" || item.TenThongSo == "TỔNG LƯU LƯỢNG 4" || item.TenThongSo == "TỔNG LƯU LƯỢNG 5" || item.TenThongSo == "ÁP LỰC 2")
                            {
                                var nextItem = ListNhatKyNam.FirstOrDefault(x => x.Thoi_Gian == item.Thoi_Gian.AddMinutes(-5) && x.TenThongSo == item.TenThongSo && x.TenTram == item.TenTram);
                                if (nextItem != null)
                                {
                                    var tieuthu = (decimal.Parse(item.Gia_Tri) - decimal.Parse(nextItem.Gia_Tri)) * 10;
                                    row.Append(ConstructCell(tieuthu.ToString(), CellValues.String, 1));
                                }
                                else
                                {
                                    row.Append(ConstructCell("--", CellValues.String, 1));
                                }
                            }
                            j++;
                        }
                    }
                    sheetData.AppendChild(row);
                    i++;
                }
                worksheetPart.Worksheet.Save();
            }
            return "UploadFiles/FileTam/" + FileName;
        }

        private Cell ConstructCell(string value, CellValues dataType)
        {
            Cell cell = new Cell()
            {
                DataType = dataType,
                CellValue = new CellValue(value)
            };
            return cell;
        }



        #region Hàm hỗ trợ
        private static Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 13 },
                    new FontName() { Val = "Times New Roman" }
                ),
                new Font( // Index 1 - header
                    new FontSize() { Val = 13 },
                    new FontName() { Val = "Times New Roman" },
                    new Bold() { Val = true },
                    new Color() { Rgb = "FFFFFF" }
                ),
                new Font( // Index 2 - tiêu đề trang
                    new FontSize() { Val = 22 },
                    new FontName() { Val = "Times New Roman" },
                    new Bold() { Val = true }
                ),
                new Font( // Index 3 - tổng số lượng
                    new FontSize() { Val = 13 },
                    new FontName() { Val = "Times New Roman" },
                    new Bold() { Val = true }
                ),
                    new Font( // Index 4 - thời gian
                    new FontSize() { Val = 8 },
                    new FontName() { Val = "Times New Roman" }
                ),
                    new Font( // Index 5 - tiêu đề trang
                    new FontSize() { Val = 14 },
                    new FontName() { Val = "Times New Roman" },
                      new Bold() { Val = true }
                ),
                    new Font( // Index 6 - thời gian
                    new FontSize() { Val = 10 },
                    new FontName() { Val = "Times New Roman" },
                    new Bold() { Val = true }
                )
                );

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = "A5A5A5" }) { PatternType = PatternValues.Solid }), // Index 2 - header
                    new Fill(new PatternFill(new ForegroundColor { Rgb = "EBEBEB" }) { PatternType = PatternValues.Solid }), // Index 3 - backgroup gray
                    new Fill(new PatternFill(new ForegroundColor { Rgb = "00ffff" }) { PatternType = PatternValues.Solid }), // Index 4 - backgroup blue
                    new Fill(new PatternFill(new ForegroundColor { Rgb = "eee8aa" }) { PatternType = PatternValues.Solid }) // Index 5 - backgroup yelow
                );

            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats();
            cellFormats.Append(new CellFormat());
            // body nomal - 1
            var cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // header - 2
            cellFormat = new CellFormat();
            cellFormat.FontId = 1;
            cellFormat.FillId = 2;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyFill = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // datetime body - 3
            cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 22;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);
            // number body - 4
            cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 3;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // Tiêu đề trang - 5
            cellFormat = new CellFormat();
            cellFormat.FontId = 2;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 0;
            cellFormat.NumberFormatId = 0;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormats.Append(cellFormat);

            // Tổng header - 6
            cellFormat = new CellFormat();
            cellFormat.FontId = 3;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                Horizontal = HorizontalAlignmentValues.Center,
                WrapText = true
            };

            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormats.Append(cellFormat);

            // Tổng number - 7
            cellFormat = new CellFormat();
            cellFormat.FontId = 3;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 3;

            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // body nomal - 8
            cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 3;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                Horizontal = HorizontalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // body nomal - 9
            cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 3;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                Horizontal = HorizontalAlignmentValues.Right,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // body Left - 10
            cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 3;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                Horizontal = HorizontalAlignmentValues.Left,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // datetime body - 11
            cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 22;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Vertical = VerticalAlignmentValues.Center,
                Horizontal = HorizontalAlignmentValues.Left,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // background gray - 12
            cellFormat = new CellFormat();
            cellFormat.FontId = 3;
            cellFormat.FillId = 3;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyFill = true;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true,

            };
            cellFormats.Append(cellFormat);


            // custom background - 13
            cellFormat = new CellFormat();
            cellFormat.FontId = 6;
            cellFormat.FillId = 4;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyFill = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // thông tin công ty - 14
            cellFormat = new CellFormat();
            cellFormat.FontId = 0;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 0;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyFill = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // textcheckin - 15
            cellFormat = new CellFormat();
            cellFormat.FontId = 4;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyBorder = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // texttitlecheckin - 16
            cellFormat = new CellFormat();
            cellFormat.FontId = 4;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyFill = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // title excel - 17
            cellFormat = new CellFormat();
            cellFormat.FontId = 5;
            cellFormat.FillId = 0;
            cellFormat.BorderId = 0;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyFill = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            // custom background - 18
            cellFormat = new CellFormat();
            cellFormat.FontId = 4;
            cellFormat.FillId = 5;
            cellFormat.BorderId = 1;
            cellFormat.NumberFormatId = 0;
            cellFormat.ApplyFill = true;
            cellFormat.ApplyNumberFormat = true;
            cellFormat.ApplyAlignment = true;
            cellFormat.Alignment = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            cellFormats.Append(cellFormat);

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }
        private static Cell ConstructCell(string value, CellValues dataType, uint styleIndex = 0)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex
            };
        }
        private string GetExcelValue(Cell c, SharedStringTable sst)
        {
            var data = "";
            if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
            {
                int ssid = int.Parse(c.CellValue.Text);
                string str = sst.ChildElements[ssid].InnerText;
                data = str;
            }
            else if (c.CellValue != null)
            {
                data = c.CellValue.Text;
            }

            return data;
        }
        #endregion
    }
}

