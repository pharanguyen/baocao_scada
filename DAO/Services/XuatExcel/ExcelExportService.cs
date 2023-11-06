using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DAO.Services.XuatExcel
{
    public class ExcelExportService
    {

        public static string BaoCaoNgay()
        {
           

            var FileName = "Danh sách cuộc họp.xlsx";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles\\FileTam");
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

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles\\FileTam", FileName);

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(FilePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                // Adding style
                WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylePart.Stylesheet = GenerateStylesheet();
                stylePart.Stylesheet.Save();

                // Setting up columns
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
                            Width = 55,
                            CustomWidth = true
                        },
                        new Column // Đơn vị tổ chức
                        {
                            Min = 3,
                            Max = 4,
                            Width = 25,
                            CustomWidth = true
                        },
                        new Column // Thờ gian bắt đầu
                        {
                            Min = 5,
                            Max = 6,
                            Width = 11,
                            CustomWidth = true
                        },
                        new Column // Thờ gian kết thúc
                        {
                            Min = 6,
                            Max = 7,
                            Width = 11,
                            CustomWidth = true
                        },
                        new Column // Trạng thái
                        {
                            Min = 6,
                            Max = 7,
                            Width = 12,
                            CustomWidth = true
                        });

                worksheetPart.Worksheet.AppendChild(columns);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                MergeCells mergeCells = new MergeCells();
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:F1") });

                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);

                // Constructing header
                Row row = new Row();
                row.Append(
                    ConstructCell("Thống kê điểm danh", CellValues.String, 5));
                row.CustomHeight = true;
                row.Height = 40;
                sheetData.AppendChild(row);

                row = new Row();
                row.Append(
                    ConstructCell("STT", CellValues.String, 2),
                    ConstructCell("Tên cuộc họp", CellValues.String, 2),
                    ConstructCell("Đơn vị tổ chức", CellValues.String, 2),
                    ConstructCell("Thời gian bắt đầu", CellValues.String, 2),
                    ConstructCell("Thời gian điểm danh", CellValues.String, 2),
                    ConstructCell("Trạng thái", CellValues.String, 2));
                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);
                var STT = 1;
                // Inserting each data
                for(int i  = 0; i< 5; i++)
                {
                    row = new Row();
                    row.Append(
                        ConstructCell(STT.ToString(), CellValues.Number, 4),
                        ConstructCell("a", CellValues.String, 1),
                        ConstructCell("a1", CellValues.String, 1),
                        ConstructCell("a2", CellValues.String, 1),
                        ConstructCell("a3", CellValues.String, 1),
                        ConstructCell("a4", CellValues.String, 1)
                        );

                    sheetData.AppendChild(row);
                }
                //foreach (var item in ListData)
                //{
                //    row = new Row();
                //    row.Append(
                //        ConstructCell(STT.ToString(), CellValues.Number, 4),
                //        ConstructCell(item.Title, CellValues.String, 1),
                //        ConstructCell(item.Department, CellValues.String, 1),
                //        ConstructCell(item.txtStartTime, CellValues.String, 1),
                //        ConstructCell(item.txtEndTime, CellValues.String, 1),
                //        ConstructCell(item.TxtStatus, CellValues.String, 1)
                //        );

                //    STT++;
                //    sheetData.AppendChild(row);
                //}

                worksheetPart.Worksheet.Save();
            }

            return "UploadFiles/FileTam/" + FileName;
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

