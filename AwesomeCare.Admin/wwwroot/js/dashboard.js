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


function getcareobj(carePId, careCId, careLId,ctxCare) {
        $.ajax({
            type: 'GET',
            url: '/Dashboard/CareObj',
            data: { 'carePId': carePId, 'careCId': careCId, 'careLId': careLId },
            success: function (response) {
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
                console.log(care);
                var Care = new Chart(ctxCare, {
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

function gettelehealth(carePId, careCId, careLId, ctxCare) {
    $.ajax({
        type: 'GET',
        url: '/Dashboard/CareObj',
        data: { 'carePId': carePId, 'careCId': careCId, 'careLId': careLId },
        success: function (response) {
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
            console.log(care);
            var Care = new Chart(ctxCare, {
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

function getongoing(carePId, careCId, careLId, ctxCare) {
    $.ajax({
        type: 'GET',
        url: '/Dashboard/CareObj',
        data: { 'carePId': carePId, 'careCId': careCId, 'careLId': careLId },
        success: function (response) {
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
            console.log(care);
            var Care = new Chart(ctxCare, {
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

function getpindicator(carePId, careCId, careLId, ctxCare) {
    $.ajax({
        type: 'GET',
        url: '/Dashboard/CareObj',
        data: { 'carePId': carePId, 'careCId': careCId, 'careLId': careLId },
        success: function (response) {
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
            console.log(care);
            var Care = new Chart(ctxCare, {
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

function getSupervision(pId, cId, lId, ctx) {
    console.log(pId + '' + cId + '' + lId)
    $.ajax({
        type: 'GET',
        url: '/Dashboard/Supervision',
        data: { 'pId': pId, 'cId': cId, 'lId': lId },
        success: function (response) {
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
            console.log(care);
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