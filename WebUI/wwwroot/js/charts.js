function createPieChart(svgId, data, colors) {
    const svg = document.getElementById(svgId);
    svg.innerHTML = '';
    
    const radius = 150;
    const centerX = 200;
    const centerY = 200;
    let total = data.reduce((sum, item) => sum + item.value, 0);
    let currentAngle = -90;

    data.forEach((item, index) => {
        // Calculate slice angles
        const angle = (item.value / total) * 360;
        const endAngle = currentAngle + angle;
        
        // Create path
        const path = document.createElementNS("http://www.w3.org/2000/svg", "path");
        const startRad = currentAngle * Math.PI / 180;
        const endRad = endAngle * Math.PI / 180;
        
        const x1 = centerX + radius * Math.cos(startRad);
        const y1 = centerY + radius * Math.sin(startRad);
        const x2 = centerX + radius * Math.cos(endRad);
        const y2 = centerY + radius * Math.sin(endRad);

        const largeArc = angle > 180 ? 1 : 0;
        const pathData = `M ${centerX},${centerY} L ${x1},${y1} A ${radius},${radius} 0 ${largeArc},1 ${x2},${y2} Z`;
        
        path.setAttribute("d", pathData);
        path.setAttribute("fill", colors[index % colors.length]);
        path.classList.add("pie-slice");
        path.dataset.tooltip = `${item.label}: ${((item.value / total) * 100).toFixed(1)}%`;
        
        // Add hover effect
        path.addEventListener('mouseover', function() {
            this.style.filter = 'brightness(120%)';
            showTooltip(this.dataset.tooltip);
        });
        
        path.addEventListener('mouseout', function() {
            this.style.filter = '';
            hideTooltip();
        });

        svg.appendChild(path);

        // Add labels
        const labelAngle = currentAngle + angle / 2;
        const labelRadius = radius * 0.6;
        const labelX = centerX + labelRadius * Math.cos(labelAngle * Math.PI / 180);
        const labelY = centerY + labelRadius * Math.sin(labelAngle * Math.PI / 180);

        const label = document.createElementNS("http://www.w3.org/2000/svg", "text");
        label.setAttribute("x", labelX);
        label.setAttribute("y", labelY);
        label.setAttribute("text-anchor", "middle");
        label.classList.add("pie-label");
        label.textContent = item.label;

        svg.appendChild(label);

        currentAngle = endAngle;
    });
}

function showTooltip(text) {
    let tooltip = document.getElementById('chart-tooltip');
    if (!tooltip) {
        tooltip = document.createElement('div');
        tooltip.id = 'chart-tooltip';
        tooltip.style.position = 'absolute';
        tooltip.style.background = 'rgba(0, 0, 0, 0.8)';
        tooltip.style.color = 'white';
        tooltip.style.padding = '5px 10px';
        tooltip.style.borderRadius = '4px';
        tooltip.style.pointerEvents = 'none';
        document.body.appendChild(tooltip);
    }
    tooltip.textContent = text;
}

function hideTooltip() {
    const tooltip = document.getElementById('chart-tooltip');
    if (tooltip) tooltip.remove();
}