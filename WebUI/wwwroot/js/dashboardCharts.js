
// Initialize the activity chart
export function initializeActivityChart(chartId, labels, data) {
    console.log("Initializing Activity Chart...");
    const ctx = document.getElementById(chartId).getContext('2d');
    return new window.Chart(ctx, {  
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Votes Cast',
                data: data,
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                fill: true,
                tension: 0.4
            }]
        },
        options: {
            responsive: true
        }
    });
}


export function initializeDemographicsChart(chartId, labels, data) {
    console.log("Initializing Demographics Chart...");
    const canvas = document.getElementById(chartId);
    if (!canvas) {
        console.error(`Canvas element with ID "${chartId}" not found.`);
        return null;
    }
    const ctx = canvas.getContext('2d');
    if (!ctx) {
        console.error("Failed to get 2D context for demographics chart.");
        return null;
    }
    return new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: data,
                backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0'],
                hoverOffset: 4
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: true,
                    position: 'bottom', // key
                    labels: {
                        usePointStyle: true,
                        padding: 20
                    }
                },
                tooltip: {
                    enabled: true
                }
            }
        }
    });
}


// Update the activity chart with new data
export function updateActivityChart(chart, labels, data) {
    chart.data.labels = labels;
    chart.data.datasets[0].data = data;
    chart.update();
}
