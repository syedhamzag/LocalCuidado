function getInvolving(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_'+name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/'+name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.involvingParties.forEach(function (result, index) {
                    var row = '<tr><td>' + (index+1) + '</td><td>' + result.name + '</td><td>' + result.address + '</td><td>' + result.email + '</td><td>' + result.telephone + '</td><td>' + result.relationship + '</td></tr>';
                    $('#tbl_'+name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getdutyoncall(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getDutyOnCall.forEach(function (result, index) {
                    var row = '<tr><td>' + result.dateOfCall + '</td><td>' + result.refNo + '</td><td>' + result.subject + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="'+result.attachment+'" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function gethospitalentry(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHospitalEntry.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.reference + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function gethospitalexit(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHospitalExit.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.reference + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getfilesrecord(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getFilesAndRecord.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.subject + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getcomplaintregister(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientComplain.forEach(function (result, index) {
                    data.push([result.dATERECIEVED, result.dUEDATE, result.iNCIDENTDATE, result.reference, result.rEMARK, result.cOMPLAINANTCONTACT, result.aCTIONTAKEN, '<a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.evidenceFilePath + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>']);

                });
                $('#tbl_' + name).DataTable().clear().rows.add(data).draw();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
}
function getlogaudit(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientLogAudit.forEach(function (result, index) {
                    data.push([result.date, result.nextDueDate, result.deadline, result.reference, result.remarks,result.actionTaken ,result.actionRecommended,'<a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.evidenceOfActionTaken + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>']);
                });
                $('#tbl_' + name).DataTable().clear().rows.add(data).draw();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
}
function getmedaudit(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientMedAudit.forEach(function (result, index) {
                    data.push([result.date, result.nextDueDate, result.deadline, result.reference, result.remarks, result.actionTaken, result.actionRecommended, '<a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.evidenceOfActionTaken + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>']);
                });
                $('#tbl_' + name).DataTable().clear().rows.add(data).draw();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
}
function getvoice(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientVoice.forEach(function (result, index) {
                    data.push([result.date, result.nextCheckDate, result.deadline, result.reference, result.remarks, result.actionRequired, '<a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>']);
                });
                $('#tbl_' + name).DataTable().clear().rows.add(data).draw();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
}
function getmgtvisit(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientMgtVisit.forEach(function (result, index) {
                    data.push([result.date, result.nextCheckDate, result.deadline, result.reference, result.remarks, result.actionRequired, '<a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>']);
                });
                $('#tbl_' + name).DataTable().clear().rows.add(data).draw();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
}
function getprogram(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientProgram.forEach(function (result, index) {
                    data.push([result.date, result.nextCheckDate, result.deadline, result.reference, result.remarks, result.actionRequired, '<a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>']);
                });
                $('#tbl_' + name).DataTable().clear().rows.add(data).draw();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
}
function getservicewatch(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientServiceWatch.forEach(function (result, index) {
                    data.push([result.date, result.nextCheckDate, result.deadline, result.reference, result.remarks, result.actionRequired, '<a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>']);
                });
                $('#tbl_' + name).DataTable().clear().rows.add(data).draw();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });

}
function getbloodcoag(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBloodCoagulationRecord.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getbloodpressure(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBloodPressure.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getbmichart(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBMIChart.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getbodytemp(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBodyTemp.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getbowel(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBowelMovement.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function geteyehealth(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientEyeHealthMonitoring.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getfoodintake(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientFoodIntake.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getheartrate(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientHeartRate.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getoxygen(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientOxygenLvl.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getpainchart(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientPainChart.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getpulserate(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientPulseRate.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getseizure(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientSeizure.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.remarks + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getwoundcare(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientWoundCare.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.comment + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getpersonaldetail(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getReview.forEach(function (result, index) {
                    var row = '<tr><td>' + result.cP_PreDate + '</td><td>' + result.cP_ReviewDate + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/PersonalDetail/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/PersonalDetail/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/PersonalDetail/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getpets(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getPets.forEach(function (result, index) {
                    var row = '<tr><td>' + result.name + '</td><td>' + result.age + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/Pets/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/Pets/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/Pets/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getintandobj(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getInterestAndObjective.forEach(function (result, index) {
                    var row = '<tr><td>' + result.careGoal + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/InterestAndObjective/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/InterestAndObjective/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/InterestAndObjective/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getpersonalhygiene(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getPersonalHygiene.forEach(function (result, index) {
                    var row = '<tr><td>' + result.laundrySupport + '</td><td>' + result.laundrySupport + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/PersonalHygiene/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/PersonalHygiene/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/PersonalHygiene/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getinfectioncontrol(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getInfectionControl.forEach(function (result, index) {
                    var row = '<tr><td>' + result.testDate + '</td><td>' + result.remarks + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/InfectionControl/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/InfectionControl/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/InfectionControl/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getmtask(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getManagingTasks.forEach(function (result, index) {
                    var row = '<tr><td>' + result.help + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/ManagingTasks/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/ManagingTasks/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/ManagingTasks/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getnutrition(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getCarePlanNutrition.forEach(function (result, index) {
                    var row = '<tr><td>' + result.specialDiet + '</td><td>' + result.avoidFood + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/CarePlanNutrition/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/CarePlanNutrition/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/CarePlanNutrition/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getbalance(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getBalance.forEach(function (result, index) {
                    var row = '<tr><td>' + result.name + '</td><td>' + result.description + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/Balance/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/Balance/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/Balance/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getphysicalability(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getPhysicalAbility.forEach(function (result, index) {
                    var row = '<tr><td>' + result.name + '</td><td>' + result.description + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/PhysicalAbility/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/PhysicalAbility/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/PhysicalAbility/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function gethealthliving(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHealthAndLiving.forEach(function (result, index) {
                    var row = '<tr><td>' + result.briefHealth + '</td><td>' + result.wakeUp + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/HealthLiving/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/HealthLiving/View?clientId=' + clientId + '">View</a>';
                    var del = '<a class="dropdown-item" href="/HealthLiving/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');

                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);
                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getspecialhealthmed(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getSpecialHealthAndMedication.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.by + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/SpecialHealthAndMedication/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/SpecialHealthAndMedication/View?clientId=' + clientId + '">View</a>';
                    var del = '<a class="dropdown-item" href="/SpecialHealthAndMedication/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getspecialhealthcond(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getSpecialHealthCondition.forEach(function (result, index) {
                    var row = '<tr><td>' + result.conditionName + '</td><td>' + result.sourceInformation + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/SpecialHealthCondition/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/SpecialHealthCondition/View?clientId=' + clientId + '">View</a>';
                    var del =  '<a class="dropdown-item" href="/SpecialHealthCondition/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function gethistoryoffall(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHistoryOfFall.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.cause + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var edit = '<a class="dropdown-item" href="/SpecialHealthAndMedication/Index?clientId=' + clientId + '">Edit</a>';
                    var view = '<a class="dropdown-item" href="/SpecialHealthAndMedication/View?clientId=' + clientId + '">View</a>';
                    var del = '<a class="dropdown-item" href="/SpecialHealthAndMedication/Delete?clientId=' + clientId + '">Delete</a>';
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
                    $(menu).append(edit);
                    $(menu).append(view);
                    $(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function gethomerisk(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(client);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHomeRiskAssessment.forEach(function (result, index) {
                    var row = '<tr><td>' + result.heading + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getdailytask(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(name);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                console.log(response.getClientDailyTask);
                response.getClientDailyTask.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.amendmentDate + '</td><td style="text-align:justify; white-space:normal;">' + result.dailyTaskName + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getmcabest(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getBestInterestAssessment.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.name + '</td><td>' + result.signature + '</td><td>' + result.position + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getcareobj(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    console.log(name);
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                console.log(response.getClientCareObj);
                response.getClientCareObj.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.note + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function setPin(element)
{
    var pin = element.value;
    $('#Pin').val(pin);
}
function getPersonalInfo(clientId)
{
    var hobby = $('#tbl_hobby').children().length;
    var health = $('#tbl_healthcon').children().length;
    if (hobby <= 0)
    {
        $.ajax({
            type: 'GET',
            url: '/Client/personalInfo',
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientHobbies.forEach(function (result, index) {
                    var row = '<tr><td>' + result.name + '</td></tr>';
                    $('#tbl_hobby').append(row);
                });
                if (health <= 0)
                { 
                    response.getClientHealthCondition.forEach(function (result, index) {
                        var row = '<tr><td>' + result.name + '</td></tr>';
                        $('#tbl_healthcon').append(row);
                    
                        //var edit = '<a class="dropdown-item" href="/ManagingTasks/Index?clientId=' + clientId + '">Edit</a>';
                        //var view = '<a class="dropdown-item" href="/ManagingTasks/View?clientId=' + clientId + '">View</a>';
                        //var del = '<a class="dropdown-item" href="/ManagingTasks/Delete?clientId=' + clientId + '">Delete</a>';
                        //var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                        //$(menu).children().remove();
                        //$(menu).append(edit);
                        //$(menu).append(view);
                        //$(menu).append(del);

                    });
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function getclientperformance(element)
{
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    //if (client <= 0) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/Client/' + name,
    //        data: { 'clientId': clientId },
    //        success: function (response) {
    //            response.getPerformanceIndicator.forEach(function (result, index) {
    //                var row = '<tr><td>' + result.help + '</td></tr>';
    //                $('#tbl_' + name).append(row);
    //                //var edit = '<a class="dropdown-item" href="/ManagingTasks/Index?clientId=' + clientId + '">Edit</a>';
    //                //var view = '<a class="dropdown-item" href="/ManagingTasks/View?clientId=' + clientId + '">View</a>';
    //                //var del = '<a class="dropdown-item" href="/ManagingTasks/Delete?clientId=' + clientId + '">Delete</a>';
    //                //var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
    //                //$(menu).children().remove();
    //                //$(menu).append(edit);
    //                //$(menu).append(view);
    //                //$(menu).append(del);

    //            });
    //        },
    //        error: function () {
    //            alert('Failed to receive the Data');
    //            console.log('Failed ');
    //        }
    //    });
    //}

}

var url_string = window.location.href;
var url = new URL(url_string);
var ActiveTab = url.searchParams.get("ActiveTab");
if (ActiveTab != null) {
    $(document).ready(function () {
        $('a[href="#' + ActiveTab + '"]').tab('show');

    });
}
$('.showfile-btn').on('click', function () {
    const showBtn = $(this);
    const Id = showBtn.data('id');
    window.open(Id);
});

function geteducation(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response.certificateAttachment);
                response.education.forEach(function (result, index) {
                    var row = '<tr><td>' + result.institution + '</td><td>' + result.certificate + '</td><td>' + result.location + '</td><td>' + result.address + '</td><td>' + result.startDate + '</td><td>' + result.endDate + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.certificateAttachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}
function gettrainingmatrix(element){
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffTrainingMatrix[0].getTrainingMatrixList.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function gettrainings(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.trainings.forEach(function (result, index) {
                    var row = '<tr><td>' + result.training + '</td><td>' + result.certificate + '</td><td>' + result.location + '</td><td>' + result.trainer + '</td><td>' + result.startDate + '</td><td>' + result.expiredDate + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getreferee(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.references.forEach(function (result, index) {
                    var row = '<tr><td>' + result.referee + '</td><td>' + result.companyName + '</td><td>' + result.address + '</td><td>' + result.phoneNumber + '</td><td>' + result.email + '</td><td>' + result.positionofReferee + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getemergencyContact(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.emergencyContacts.forEach(function (result, index) {
                    var row = '<tr><td>' + result.contactName + '</td><td>' + result.telephone + '</td><td>' + result.email + '</td><td>' + result.relationship + '</td><td>' + result.address + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getspotcheck(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffSpotCheck.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.areaComments + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getadlobs(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffAdlObs.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getmedcomp(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffMedComp.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getkeyworker(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffKeyWorkerVoice.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getsurvey(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffSurvey.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getonetoone(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffOneToOne.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getsupervision(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffSupervisionAppraisal.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getreference(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffReference.forEach(function (result, index) {
                    var row = '<tr><td>' + result.date + '</td><td>' + result.dateofEmployement + '</td><td>' + result.contact + '</td><td>' + result.email + '</td><td>' + result.address + '</td><td>' + result.rehireStaff + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getpersonalitytest(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.forEach(function (result, index) {
                    var row = '<tr><td>' + result.questionName + '</td><td>' + result.answerName + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getinfection(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.forEach(function (result, index) {
                    var row = '<tr><td>' + result.testDate + '</td><td>' + result.guideline + '</td><td>' + result.remarks + '</td><td>' + result.infectionName + '</td><td>' + result.vaccName + '</td><td>' + result.typeName + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getcompetence(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.forEach(function (result, index) {
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function gethealth(element){
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.forEach(function (result, index) {
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getinterview(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.forEach(function (result, index) {
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getshadowing(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.forEach(function (result, index) {
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getstaffholiday(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (result) {
                    var row = '<tr><td>' + result.staffName + '</td><td>' + result.className + '</td><td>' + result.startDate + '</td><td>' + result.endDate + '</td><td>' + result.allocatedDays + '</td><td>' + result.balance + '</td></tr>';
                    $('#tbl_' + name).append(row);
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getsalaryallowance(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getStaffTrainingMatrix.forEach(function (result, index) {
                    var row = '<tr><td>' + result.startDate + '</td><td>' + result.endDate + '</td><td>' + result.percentage + '</td><td>' + result.amount + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
function getsalarydeduction(element) {
    var staffId = element.id;
    var name = element.href.split('#')[1];
    var staff = $('#tbl_' + name).children().length;
    console.log(name);
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response);
                response.getSalaryDeduction.forEach(function (result, index) {
                    var row = '<tr><td>' + result.startDate + '</td><td>' + result.endDate + '</td><td>' + result.percentage + '</td><td>' + result.amount + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
