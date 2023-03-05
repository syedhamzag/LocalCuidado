var clientColor = [
    'rgba(255, 193, 7, 1)',
    'rgba(40, 167, 69, 1)',
    'rgba(220, 53, 69, 1)',
    'rgba(0, 123, 255, 1)'
];
var hidelabel = {
    scales: {
        yAxes: [{
            ticks: {
                min: 0,
                beginAtZero: true
            },
            gridLines: {
                display: false,
            }
        }]
    }
};


function getcareobj(carePId, careCId, careLId, ctxChart, clientId) {
        $.ajax({
            type: 'GET',
            url: '/Dashboard/CareObj',
            data: { 'carePId': carePId, 'careCId': careCId, 'careLId': careLId, 'clientId': clientId },
            success: function (response) {
                var canvas = document.getElementById(ctxChart);
                var ctx = canvas.getContext('2d');
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                var labels = [];
                var data = [];
                response.forEach(function (result, index) { labels.push(result.key) });
                response.forEach(function (result, index) { data.push(result.value) });
                var care = {
                    labels: labels,
                    datasets: [{
                        label: "Chart",
                        backgroundColor: clientColor,
                        borderColor: clientColor,
                        borderWidth: 0,
                        data: data
                    }]
                };
                var cChart = new Chart(ctx, {
                    options: hidelabel,
                    data: care,
                    type: 'pie'

                });
                cChart.render();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
}

function gettelehealth(nId, oId, aId, rId, ctxChart, clientId) {
    $.ajax({
        type: 'GET',
        url: '/Dashboard/Telehealth',
        data: { 'nId': nId, 'oId': oId, 'aId': aId, 'rId': rId, 'clientId': clientId },
        success: function (response) {
            var canvas = document.getElementById(ctxChart);
            var ctx = canvas.getContext('2d');
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            var labels = [];
            var data = [];
            response.teleHealth.forEach(function (result, index) { labels.push(result.key) });
            response.teleHealth.forEach(function (result, index) { data.push(result.value) });
            var care = {
                labels: labels,
                datasets: [{
                    label: "Chart",
                    backgroundColor: clientColor,
                    borderColor: clientColor,
                    borderWidth: 0,
                    data: data
                }]
            };
            var Care = new Chart(ctx, {
                options: hidelabel,
                data: care,
                type: 'pie'

            });
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    });
}

function getongoing(carePId, careCId, careLId, ctxChart, clientId) {
    $.ajax({
        type: 'GET',
        url: '/Dashboard/Ongoing',
        data: { 'carePId': carePId, 'careCId': careCId, 'careLId': careLId, 'clientId': clientId },
        success: function (response) {
            var canvas = document.getElementById(ctxChart);
            var ctx = canvas.getContext('2d');
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            var labels = [];
            var data = [];
            response.forEach(function (result, index) { labels.push(result.key) });
            response.forEach(function (result, index) { data.push(result.value) });
            var care = {
                labels: labels,
                datasets: [{
                    label: "Chart",
                    backgroundColor: clientColor,
                    borderColor: clientColor,
                    borderWidth: 0,
                    data: data
                }]
            };
            var Care = new Chart(ctx, {
                options: hidelabel,
                data: care,
                type: 'pie'

            });
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    });
}

function getpindicator(carePId, careCId, careLId, ctxChart, clientId) {
    $.ajax({
        type: 'GET',
        url: '/Dashboard/Performance',
        data: { 'carePId': carePId, 'careCId': careCId, 'careLId': careLId, 'clientId': clientId },
        success: function (response) {
            var canvas = document.getElementById(ctxChart);
            var ctx = canvas.getContext('2d');
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            var labels = [];
            var data = [];
            response.clientMatrix.forEach(function (result, index) { labels.push(result.key) });
            response.clientMatrix.forEach(function (result, index) { data.push(result.value) });
            var care = {
                labels: labels,
                datasets: [{
                    label: "Chart",
                    backgroundColor: clientColor,
                    borderColor: clientColor,
                    borderWidth: 0,
                    data: data
                }]
            };
            var Care = new Chart(ctx, {
                options: hidelabel,
                data: care,
                type: 'pie'

            });
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    });
}

function getSupervision(pId, cId, lId, ctxChart) {
    console.log(pId + '' + cId + '' + lId)
    $.ajax({
        type: 'GET',
        url: '/Dashboard/Supervision',
        data: { 'pId': pId, 'cId': cId, 'lId': lId },
        success: function (response) {
            var canvas = document.getElementById(ctxChart);
            var ctx = canvas.getContext('2d');
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            var labels = [];
            var data = [];
            response.forEach(function (result, index) { labels.push(result.key) });
            response.forEach(function (result, index) { data.push(result.value) });
            var care = {
                labels: labels,
                datasets: [{
                    label: "Chart",
                    backgroundColor: clientColor,
                    borderColor: clientColor,
                    borderWidth: 0,
                    data: data
                }]
            };
            var Care = new Chart(ctx, {
                options: hidelabel,
                data: care,
                type: 'pie'

            });
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    });
}