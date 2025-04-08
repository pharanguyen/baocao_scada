console.log("chart-setup.js loaded");

window.initializeChart = () => {
    console.log("initializeChart called");

    const ctx = document.getElementById('myChart').getContext('2d');
    window.myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: Array.from({ length: 24 }, (_, i) => `${i}h`), // 0h-23h
            datasets: [{
                label: 'Sản Lượng',
                data: Array(24).fill(0), // Khởi tạo dữ liệu là 0
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Sản Lượng'
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Thời Gian'
                    }
                }
            }
        }
    });

    const data = {
        tuyen: {
            tuyen1: { tram1: generateRandomData(), tram2: generateRandomData() },
            tuyen2: { tram3: generateRandomData(), tram4: generateRandomData() }
        },
        chinhanh: {
            chinhanh1: { tram1: generateRandomData(), tram2: generateRandomData() },
            chinhanh2: { tram3: generateRandomData(), tram4: generateRandomData() }
        }
    };

    function generateRandomData() {
        return Array.from({ length: 24 }, () => Math.floor(Math.random() * 100));
    }

    document.getElementById('type').addEventListener('change', updateOptions);
    document.getElementById('options').addEventListener('change', updateChart);

    function updateOptions() {
        const type = document.getElementById('type').value;
        const optionsSelect = document.getElementById('options');
        optionsSelect.innerHTML = ''; // Xóa các tùy chọn hiện tại
        const options = data[type];
        for (const key in options) {
            const optgroup = document.createElement('optgroup');
            optgroup.label = key;
            for (const tram in options[key]) {
                const option = document.createElement('option');
                option.value = `${key}-${tram}`;
                option.text = tram;
                optgroup.appendChild(option);
            }
            optionsSelect.appendChild(optgroup);
        }
    }

    function updateChart() {
        const selectedOptions = Array.from(document.getElementById('options').selectedOptions).map(option => option.value);
        const type = document.getElementById('type').value;
        let newData = Array(24).fill(0);

        selectedOptions.forEach(option => {
            const [group, tram] = option.split('-');
            const tramData = data[type][group][tram];
            newData = newData.map((value, index) => value + tramData[index]);
        });

        window.myChart.data.datasets[0].data = newData;
        window.myChart.update();
    }

    // Khởi tạo lần đầu
    updateOptions();
};
