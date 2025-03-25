// // charts.js


// export function createResultsChart(canvasId, chartData) {
//     const ctx = document.getElementById(canvasId).getContext('2d');
    
//     return new Chart(ctx, {
//         type: 'bar',
//         data: {
//             labels: chartData.labels,
//             datasets: [{
//                 label: `Votes for ${chartData.position}`,
//                 data: chartData.data,
//                 backgroundColor: chartData.colors,
//                 borderColor: '#2A5C8D',
//                 borderWidth: 1
//             }]
//         },
//         options: {
//             responsive: true,
//             maintainAspectRatio: false,
//             plugins: {
//                 title: {
//                     display: true,
//                     text: `Total Registered Voters: ${chartData.totalVoters}`,
//                     padding: { bottom: 20 }
//                 },
//                 legend: { display: false },
//                 tooltip: {
//                     callbacks: {
//                         label: function(context) {
//                             const percentage = (context.raw / chartData.totalVoters * 100).toFixed(1);
//                             return `${context.raw} votes (${percentage}%)`;
//                         }
//                     }
//                 }
//             },
//             scales: {
//                 y: {
//                     beginAtZero: true,
//                     max: chartData.totalVoters,
//                     title: { display: true, text: 'Number of Votes' }
//                 },
//                 x: {
//                     title: { display: true, text: 'Candidates' }
//                 }
//             }
//         }
//     });
// }
// export function destroyChart(chartInstance) {
//     if (chartInstance) {
//         chartInstance.destroy();
//     }
// }

// // Register window resize handler for Blazor components
// function registerChartResizeHandler(dotNetHelper) {
//     let resizeTimeout;
    
//     window.addEventListener('resize', () => {
//         clearTimeout(resizeTimeout);
//         resizeTimeout = setTimeout(() => {
//             dotNetHelper.invokeMethodAsync('OnChartResize');
//         }, 200); // Debounce for 200ms
//     });
// }

// // Make the function available globally
// window.chartResize = {
//     register: registerChartResizeHandler
// };