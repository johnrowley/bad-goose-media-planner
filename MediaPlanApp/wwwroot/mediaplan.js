let spendChart = null;

window.renderSpendChart = (canvasId, labels, values) => {
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    if (spendChart) {
        spendChart.destroy();
    }

    spendChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: values,
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'right' }
            }
        }
    });
};
