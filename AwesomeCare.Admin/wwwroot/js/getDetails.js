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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getDutyOnCall.forEach(function (result, index) {
                    var dateOfCall = new Date(result.dateOfCall);
                    var pin = '<input id="dutyoncall-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="dutyoncalledit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/DutyOnCall/Print?DutyOnCallId=' + result.dutyOnCallId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/DutyOnCall/Email?DutyOnCallId=' + result.dutyOnCallId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/DutyOnCall/Download?DutyOnCallId=' + result.dutyOnCallId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/DutyOnCall/Delete?DutyOnCallId=' + result.dutyOnCallId
                        + '">Delete</a>';
                    var row = '<tr><td>' + dateOfCall.toLocaleDateString() + '</td><td>' + result.refNo + '</td><td>' + result.subject + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHospitalEntry.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var pin = '<input id="hospitalentry-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="hospitalentryedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/HospitalEntry/Print?Id=' + result.hospitalEntryId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/HospitalEntry/Email?Id=' + result.hospitalEntryId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/HospitalEntry/Download?Id=' + result.hospitalEntryId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/HospitalEntry/Delete?Id=' + result.hospitalEntryId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.reference + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHospitalExit.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var pin = '<input id="hospitalexit-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="hospitalexitedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/HospitalExit/Print?Id=' + result.hospitalExitId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/HospitalExit/Email?Id=' + result.hospitalExitId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/HospitalExit/Download?Id=' + result.hospitalExitId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/HospitalExit/Delete?Id=' + result.hospitalExitId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                
                response.getFilesAndRecord.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var pin = '<input id="filesandrecord-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="filesandrecordedit" class="btn btn-primary" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/FilesAndRecord/Print?FilesAndRecordId=' + result.filesAndRecordId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/FilesAndRecord/Email?FilesAndRecordId=' + result.filesAndRecordId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/FilesAndRecord/Download?FilesAndRecordId=' + result.filesAndRecordId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/FilesAndRecord/Delete?FilesAndRecordId=' + result.filesAndRecordId + '">Delete</a>';
                    var row = '<tr><td>'+ date.toLocaleDateString() + '</td><td>' + result.subject + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientComplain.forEach(function (result, index) {
                    var daterecieved = new Date(result.daterecieved);
                    var duedate = new Date(result.duedate);
                    var incidentdate = new Date(result.incidentdate);
                    var pin = '<input id="complain-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="complainedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/Complain/Print?complainId=' + result.complainId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/Complain/Email?complainId=' + result.complainId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/Complain/Download?complainId=' + result.complainId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/Complain/Delete?complainId=' + result.complainId + '">Delete</a>';
                    var row = '<tr><td>' + daterecieved.toLocaleDateString() + '</td><td>' + duedate.toLocaleDateString() + '</td><td>' + incidentdate.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.remark + '</td><td>' + result.complainantcontact + '</td><td>' + result.actiontaken + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.evidenceFilePath + '" style="margin-left:5px;"><i class="fa fa-file"></i></a>' + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getlogaudit(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientLogAudit.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var nextDueDate = new Date(result.nextDueDate);
                    var deadline = new Date(result.deadline);
                    var pin = '<input id="clientlogaudit-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientlogauditedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientLogAudit/Print?logId=' + result.logAuditId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientLogAudit/Email?logId=' + result.logAuditId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientLogAudit/Download?logId=' + result.logAuditId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientLogAudit/Delete?logId=' + result.logAuditId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextDueDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.remarks + '</td><td>' + result.actionTaken + '</td><td>' + result.actionRecommended + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.evidenceOfActionTaken + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getmedaudit(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientMedAudit.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var nextDueDate = new Date(result.nextDueDate);
                    var deadline = new Date(result.deadline);
                    var pin = '<input id="clientmedaudit-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientmedauditedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientMedAudit/Print?medId=' + result.medAuditId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientMedAudit/Email?medId=' + result.medAuditId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientMedAudit/Download?medId=' + result.medAuditId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientMedAudit/Delete?medId=' + result.medAuditId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextDueDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.remarks + '</td><td>' + result.actionTaken + '</td><td>' + result.actionRecommended + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.evidenceOfActionTaken + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td> <td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getvoice(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientVoice.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);
                    var pin = '<input id="clientvoice-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientvoiceedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientVoice/Print?vId=' + result.voiceId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientVoice/Email?vId=' + result.voiceId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientVoice/Download?vId=' + result.voiceId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientVoice/Delete?vId=' + result.voiceId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.remarks + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getmgtvisit(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientMgtVisit.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);
                    var pin = '<input id="clientmgtvisit-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientmgtvisitedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientMgtVisit/Print?mgtId=' + result.visitId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientMgtVisit/Email?mgtId=' + result.visitId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientMgtVisit/Download?mgtId=' + result.visitId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientMgtVisit/Delete?mgtId=' + result.visitId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.remarks + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getprogram(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientProgram.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);
                    var pin = '<input id="clientprogram-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientprogramedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientProgram/Print?progId=' + result.programId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientProgram/Email?progId=' + result.programId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientProgram/Download?progId=' + result.programId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientProgram/Delete?progId=' + result.programId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.remarks + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getservicewatch(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var data = [];
                response.getClientServiceWatch.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);
                    var pin = '<input id="clientservicewatch-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientservicewatchedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientServiceWatch/Print?serviceId=' + result.watchId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientServiceWatch/Email?serviceId=' + result.watchId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientServiceWatch/Download?serviceId=' + result.watchId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientServiceWatch/Delete?serviceId=' + result.watchId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.remarks + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getbloodcoag(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBloodCoagulationRecord.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientbloodcoagulationrecord-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientbloodcoagulationrecordedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientBloodCoagulationRecord/Print?bloodId=' + result.bloodRecordId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientBloodCoagulationRecord/Email?bloodId=' + result.bloodRecordId  + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientBloodCoagulationRecord/Download?bloodId=' + result.bloodRecordId  + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientBloodCoagulationRecord/Delete?bloodId=' + result.bloodRecordId  + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBloodPressure.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientbloodpressure-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientbloodpressureedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientBloodPressure/Print?bloodId=' + result.bloodPressureId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientBloodPressure/Email?bloodId=' + result.bloodPressureId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientBloodPressure/Download?bloodId=' + result.bloodPressureId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientBloodPressure/Delete?bloodId=' + result.bloodPressureId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBMIChart.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientbmichart-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientbmichartedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientBMIChart/Print?BMIId=' + result.bMIChartId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientBMIChart/Email?BMIId=' + result.bMIChartId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientBMIChart/Download?BMIId=' + result.bMIChartId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientBMIChart/Delete?BMIId=' + result.bMIChartId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
}s
function getbodytemp(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBodyTemp.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientbodytemp-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientbodytempedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientBodyTemp/Print?TempId=' + result.bodyTempId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientBodyTemp/Email?TempId=' + result.bodyTempId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientBodyTemp/Download?TempId=' + result.bodyTempId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientBodyTemp/Delete?TempId=' + result.bodyTempId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientBowelMovement.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientbowelmovement-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientbowelmovementedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientBowelMovement/Print?bowelId=' + result.bowelMovementId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientBowelMovement/Email?bowelId=' + result.bowelMovementId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientBowelMovement/Download?bowelId=' + result.bowelMovementId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientBowelMovement/Delete?bowelId=' + result.bowelMovementId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientEyeHealthMonitoring.forEach(function (result, index) {
                    var date = new Date(result.date);
                       
                    var pin = '<input id="clienteyehealthmonitoring-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clienteyehealthmonitoringedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientEyeHealthMonitoring/Print?eyeId=' + result.eyeHealthId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientEyeHealthMonitoring/Email?eyeId=' + result.eyeHealthId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientEyeHealthMonitoring/Download?eyeId=' + result.eyeHealthId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientEyeHealthMonitoring/Delete?eyeId=' + result.eyeHealthId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientFoodIntake.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientfoodintake-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientfoodintakeedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientFoodIntake/Print?foodId=' + result.foodIntakeId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientFoodIntake/Email?foodId=' + result.foodIntakeId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientFoodIntake/Download?foodId=' + result.foodIntakeId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientFoodIntake/Delete?foodId=' + result.foodIntakeId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientHeartRate.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientheartrate-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientheartrateedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientHeartRate/Print?HeartId=' + result.heartRateId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientHeartRate/Email?HeartId=' + result.heartRateId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientHeartRate/Download?HeartId=' + result.heartRateId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientHeartRate/Delete?HeartId=' + result.heartRateId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientOxygenLvl.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientoxygenlvl-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientoxygenlvledit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientOxygenLvl/Print?oxygenId=' + result.oxygenLvlId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientOxygenLvl/Email?oxygenId=' + result.oxygenLvlId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientOxygenLvl/Download?oxygenId=' + result.oxygenLvlId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientOxygenLvl/Delete?oxygenId=' + result.oxygenLvlId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientPainChart.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientpainchart-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientpainchartedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientPainChart/Print?painId=' + result.painChartId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientPainChart/Email?painId=' + result.painChartId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientPainChart/Download?painId=' + result.painChartId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientPainChart/Delete?painId=' + result.painChartId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientPulseRate.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientpulserate-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientpulserateedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientPulseRate/Print?pulseId=' + result.pulseRateId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientPulseRate/Email?pulseId=' + result.pulseRateId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientPulseRate/Download?pulseId=' + result.pulseRateId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientPulseRate/Delete?pulseId=' + result.pulseRateId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientSeizure.forEach(function (result, index) {
                var date = new Date(result.date);

                var pin = '<input id="clientseizure-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                var edit = '<a id="clientseizureedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientSeizure/Print?SeizId=' + result.seizureId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientSeizure/Email?SeizId=' + result.seizureId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientSeizure/Download?SeizId=' + result.seizureId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientSeizure/Delete?SeizId=' + result.seizureId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.remarks + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getClientWoundCare.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientwoundcare-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientwoundcareedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientWoundCare/Print?woundId=' + result.woundCareId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientWoundCare/Email?woundId=' + result.woundCareId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientWoundCare/Download?woundId=' + result.woundCareId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientWoundCare/Delete?woundId=' + result.woundCareId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.comment + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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

function checkPIN(element)
{
    var pin = element.value;
    var name = element.id.split('-')[0];
    var clientId = element.id.split('-')[1];
    $.ajax({
        type: 'GET',
        url: '/Client/CheckPIN',
        data: { 'pin': pin },
        success: function (response) {
            if (response == "OK")
            {
                $('#' + name + 'edit').attr('href', '/' + name + '/' + 'Index?clientId=' + clientId + '');
                $('#' + name + '-' + clientId).remove();
            }
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    });
}
function getincident(element) {
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                console.log(response);
                response.getIncidentReports.forEach(function (result, index) {
                    console.log(result);
                    var pin = '<input id="incident-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="incidentedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/IncidentReporting/Print?Id=' + result.incidentReportingId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/IncidentReporting/Email?Id=' + result.incidentReportingId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/IncidentReporting/Download?Id=' + result.incidentReportingId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/IncidentReporting/Delete?Id=' + result.incidentReportingId + '">Delete</a>';
                    var row = '<tr><td>' + result.incidentType + '</td><td>' + result.staffInvolved + '</td><td>' + result.reportingStaff + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getReview.forEach(function (result, index) {
                    var cP_PreDate = new Date(result.cP_PreDate);
                    var cP_ReviewDate = new Date(result.cP_ReviewDate);
                    
                    var pin = '<input id="personaldetail-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="personaldetailedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/PersonalDetail/Print?Id=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/PersonalDetail/Email?Id=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/PersonalDetail/Download?Id=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/PersonalDetail/Delete?Id=' + clientId + '">Delete</a>';
                    
                    var row = '<tr><td>' + cP_PreDate.toLocaleDateString() + '</td><td>' + cP_ReviewDate.toLocaleDateString() + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getPets.forEach(function (result, index) {

                    var pin = '<input id="pets-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="petsedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/Pets/Print?clientId=' + result.petsId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/Pets/Email?clientId=' + result.petsId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/Pets/Download?clientId=' + result.petsId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/Pets/Delete?clientId=' + result.petsId + '">Delete</a>';
                    var row = '<tr><td>' + result.name + '</td><td>' + result.age + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
                var row = '';
                response.getInterestAndObjective.forEach(function (result, index) {
                    var pin = '<input id="interestandobjective-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="interestandobjectiveedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/InterestAndObjective/Print?clientId=' + result.goalId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/InterestAndObjective/Email?clientId=' + result.goalId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/InterestAndObjective/Download?clientId=' + result.goalId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/InterestAndObjective/Delete?Id=' + result.goalId + '">Delete</a>';
                    var row = '<tr><td>' + result.careGoal + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

                });
                $('#tbl_' + name).append(row);
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getPersonalHygiene.forEach(function (result, index) {
                    var pin = '<input id="personalhygiene-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="personalhygieneedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/PersonalHygiene/Print?clientId=' + result.hygieneId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/PersonalHygiene/Email?clientId=' + result.hygieneId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/PersonalHygiene/Download?clientId=' + result.hygieneId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/PersonalHygiene/Delete?clientId=' + result.hygieneId + '">Delete</a>';
                    var row = '<tr><td>' + result.laundrySupport + '</td><td>' + result.laundrySupport + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getInfectionControl.forEach(function (result, index) {
                    var testDate = new Date(result.testDate);

                    var pin = '<input id="infectioncontrol-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="infectioncontroledit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/InfectionControl/Print?clientId=' + result.infectionId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/InfectionControl/Email?clientId=' + result.infectionId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/InfectionControl/Download?clientId=' + result.infectionId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/InfectionControl/Delete?clientId=' + result.infectionId + '">Delete</a>';
                    var row = '<tr><td>' + testDate.toLocaleDateString() + '</td><td>' + result.remarks + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                var row = "";
                response.getManagingTasks.forEach(function (result, index) {
                    var pin = '<input id="managingtasks-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="managingtasksedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ManagingTasks/Print?clientId=' + result.taskId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ManagingTasks/Email?clientId=' + result.taskId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ManagingTasks/Download?clientId=' + result.taskId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ManagingTasks/Delete?clientId=' + result.taskId + '">Delete</a>';
                    var row = '<tr><td>' + result.help + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);
                    
                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

                });
                $('#tbl_' + name).append(row);
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
                    var pin = '<input id="careplannutrition-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="careplannutritionedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/CarePlanNutrition/Print?clientId=' + result.nutritionId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/CarePlanNutrition/Email?clientId=' + result.nutritionId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/CarePlanNutrition/Download?clientId=' + result.nutritionId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/CarePlanNutrition/Delete?clientId=' + result.nutritionId + '">Delete</a>';
                    var row = '<tr><td>' + result.specialDiet + '</td><td>' + result.avoidFood + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getBalance.forEach(function (result, index) {
                    var pin = '<input id="balance-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="balanceedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/Balance/Print?clientId=' + resuolt.balanceId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/Balance/Email?clientId=' + resuolt.balanceId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/Balance/Download?clientId=' + resuolt.balanceId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/Balance/Delete?clientId=' + resuolt.balanceId + '">Delete</a>';
                    var row = '<tr><td>' + result.name + '</td><td>' + result.description + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getPhysicalAbility.forEach(function (result, index) {
                    var pin = '<input id="physicalability-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="physicalabilityedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/PhysicalAbility/Print?clientId=' + result.physicalId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/PhysicalAbility/Email?clientId=' + result.physicalId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/PhysicalAbility/Download?clientId=' + result.physicalId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/PhysicalAbility/Delete?clientId=' + result.physicalId + '">Delete</a>';
                    var row = '<tr><td>' + result.name + '</td><td>' + result.description + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHealthAndLiving.forEach(function (result, index) {
                    var pin = '<input id="healthliving-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="healthlivingedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/HealthLiving/Print?clientId=' + result.hLId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/HealthLiving/Email?clientId=' + result.hLId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/HealthLiving/Download?clientId=' + result.hLId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/HealthLiving/Delete?clientId=' + result.hLId + '">Delete</a>';
                    var row = '<tr><td>' + result.briefHealth + '</td><td>' + result.wakeUp + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getSpecialHealthAndMedication.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="specialhealthandmedication-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="specialhealthandmedicationedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/SpecialHealthAndMedication/Print?clientId=' + result.sHMId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/SpecialHealthAndMedication/Email?clientId=' + result.sHMId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/SpecialHealthAndMedication/Download?clientId=' + result.sHMId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/SpecialHealthAndMedication/Delete?clientId=' + result.sHMId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.by + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getSpecialHealthCondition.forEach(function (result, index) {
                    var pin = '<input id="specialhealthcondition-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="specialhealthconditionedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/SpecialHealthCondition/Print?clientId=' + result.healthCondId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/SpecialHealthCondition/Email?clientId=' + result.healthCondId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/SpecialHealthCondition/Download?clientId=' + result.healthCondId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/SpecialHealthCondition/Delete?clientId=' + result.healthCondId + '">Delete</a>';
                    var row = '<tr><td>' + result.conditionName + '</td><td>' + result.sourceInformation + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHistoryOfFall.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="historyoffall-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="historyoffalledit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/HistoryOfFall/Print?clientId=' + result.historyId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/HistoryOfFall/Email?clientId=' + result.historyId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/HistoryOfFall/Download?clientId=' + result.historyId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/HistoryOfFall/Delete?clientId=' + result.historyId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.cause + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    $(menu).children().remove();

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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getHomeRiskAssessment.forEach(function (result, index) {
                    var pin = '<input id="homeriskassessment-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="homeriskassessmentedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/HomeRiskAssessment/Print?clientId=' + result.homeRiskAssessmentId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/HomeRiskAssessment/Email?clientId=' + result.homeRiskAssessmentId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/HomeRiskAssessment/Download?clientId=' + result.homeRiskAssessmentId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/HomeRiskAssessment/Delete?clientId=' + result.homeRiskAssessmentId + '">Delete</a>';
                    var row = '<tr><td>' + result.heading + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                console.log(response.getClientDailyTask);
                response.getClientDailyTask.forEach(function (result, index) {
                    var date = new Date(result.date);
                    var amendmentDate = new Date(result.amendmentDate);

                    var pin = '<input id="clientdailytask-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientdailytaskedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientDailyTask/Print?ClientDailyTaskId=' + result.dailyTaskId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientDailyTask/Email?ClientDailyTaskId=' + result.dailyTaskId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientDailyTask/Download?ClientDailyTaskId=' + result.dailyTaskId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientDailyTask/Delete?ClientDailyTaskId=' + result.dailyTaskId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + amendmentDate.toLocaleDateString() + '</td><td style="text-align:justify; white-space:normal;">' + result.dailyTaskName + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
                var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                $(menu).children().remove();
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
                    var date = new Date(result.date);
                    var pin = '<input id="bestinterestassessment-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="bestinterestassessmentedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/BestInterestAssessment/Print?clientId=' + result.bestId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/BestInterestAssessment/Email?clientId=' + result.bestId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/BestInterestAssessment/Download?clientId=' + result.bestId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/DailyTask/Delete?clientId=' + result.bestId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.name + '</td><td>' + result.signature + '</td><td>' + result.position + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);
                });
                var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                $(menu).children().remove();
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                console.log(response.getClientCareObj);
                response.getClientCareObj.forEach(function (result, index) {
                    var date = new Date(result.date);

                    var pin = '<input id="clientcareobj-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientcareobjedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientCareObj/Print?CareObjId=' + result.careObjId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientCareObj/Email?CareObjId=' + result.careObjId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientCareObj/Download?CareObjId=' + result.careObjId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientCareObj/Delete?CareObjId=' + result.careObjId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.note + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
function getcarereview(element)
{
    var clientId = element.id;
    var name = element.href.split('#')[1];
    var client = $('#tbl_' + name).children().length;
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getCareReview.forEach(function (result, index) {
                    var pin = '<input id="carereview-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="carereviewedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/CareReview/Print?Id=' + result.careReviewId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/CareReview/Email?Id=' + result.careReviewId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/CareReview/Download?Id=' + result.careReviewId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/CareReview/Delete?Id=' + result.careReviewId + '">Delete</a>';
                    var row = '<tr><td>' + result.name + '</td><td>' + result.note + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
    if (client <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Client/' + name,
            data: { 'clientId': clientId },
            success: function (response) {
                response.getPerformanceIndicator.forEach(function (result, index) {
                    var pin = '<input id="clientperformanceindicator-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="clientperformanceindicatoredit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/ClientPerformanceIndicator/Print?Id=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/ClientPerformanceIndicator/Email?Id=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/ClientPerformanceIndicator/Download?Id=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/ClientPerformanceIndicator/Delete?Id=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + result.help + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                    //var edit = '<a class="dropdown-item" href="/ManagingTasks/Index?clientId=' + clientId + '">Edit</a>';
                    //var view = '<a class="dropdown-item" href="/ManagingTasks/View?clientId=' + clientId + '">View</a>';
                    //var del = '<a class="dropdown-item" href="/ManagingTasks/Delete?clientId=' + clientId + '">Delete</a>';
                    //var menu = $('#tbl_' + name).parent().parent().children('div').children('div');
                    //$(menu).children().remove();
                    //$(menu).append(edit);
                    //$(menu).append(view);
                    //$(menu).append(del);

                });
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }

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
    if (staff <= 0) {
        $.ajax({
            type: 'GET',
            url: '/Staff/Staff' + name,
            data: { 'staffId': staffId },
            success: function (response) {
                console.log(response.certificateAttachment);
                response.education.forEach(function (result, index) {
                    var startDate = new Date(result.startDate);
                    var endDate = new Date(result.endDate);

                    var row = '<tr><td>' + result.institution + '</td><td>' + result.certificate + '</td><td>' + result.location + '</td><td>' + result.address + '</td><td>' + startDate.toLocaleDateString() + '</td><td>' + endDate.toLocaleDateString() + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.certificateAttachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td></tr>';
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
                    var date = new Date(result.date);

                    var pin = '<input id="stafftrainingmatrix-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="stafftrainingmatrixedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffTrainingMatrix/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffTrainingMatrix/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffTrainingMatrix/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffTrainingMatrix/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var startDate = new Date(result.startDate);
                    var expiredDate = new Date(result.expiredDate);

                    var row = '<tr><td>' + result.training + '</td><td>' + result.certificate + '</td><td>' + result.location + '</td><td>' + result.trainer + '</td><td>' + startDate.toLocaleDateString() + '</td><td>' + expiredDate.toLocaleDateString() + '</td></tr>';
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
                    var date = new Date(result.date);

                    var nextCheckDate = new Date(result.nextCheckDate);
                    var pin = '<input id="staffspotcheck-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffspotcheckedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffSpotCheck/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffSpotCheck/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffSpotCheck/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffSpotCheck/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + result.nextCheckDate + '</td><td>' + result.deadline + '</td><td>' + result.areaComments + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);

                    var pin = '<input id="staffadlobs-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffadlobsedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffAdlObs/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffAdlObs/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffAdlObs/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffAdlObs/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);

                    var pin = '<input id="staffmedcomp-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffmedcompedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffMedComp/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffMedComp/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffMedComp/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffMedComp/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);

                    var pin = '<input id="staffkeyworkervoice-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffkeyworkervoiceedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffKeyWorkerVoice/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffKeyWorkerVoice/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffKeyWorkerVoice/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffKeyWorkerVoice/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);

                    var pin = '<input id="staffsurvey-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffsurveyedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffSurvey/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffSurvey/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffSurvey/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffSurvey/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var date = new Date(result.date);
                    var nextCheckDate = new Date(result.nextCheckDate);
                    var deadline = new Date(result.deadline);

                    var pin = '<input id="staffonetoone-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffonetooneedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffOneToOne/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffOneToOne/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffOneToOne/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffOneToOne/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                var date = new Date(result.date);
                var nextCheckDate = new Date(result.nextCheckDate);
                var deadline = new Date(result.deadline);

                var pin = '<input id="staffsupervisionappraisal-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                var edit = '<a id="staffsupervisionappraisaledit" class="btn btn-warning" href="#">Edit</a>';
                var print = '<a class="btn btn-secondary" href="/StaffSupervisionAppraisal/Print?clientId=' + clientId + '">Print</a>';
                var email = '<a class="btn btn-success" href="/StaffSupervisionAppraisal/Email?clientId=' + clientId + '">Email</a>';
                var download = '<a class="btn btn-info" href="/StaffSupervisionAppraisal/Download?clientId=' + clientId + '">Download</a>';
                var del = '<a class="btn btn-danger" href="/StaffSupervisionAppraisal/Delete?clientId=' + clientId + '">Delete</a>';
                console.log(response);
                response.getStaffSupervisionAppraisal.forEach(function (result, index) {
                var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + nextCheckDate.toLocaleDateString() + '</td><td>' + deadline.toLocaleDateString() + '</td><td>' + result.reference + '</td><td>' + result.details + '</td><td>' + result.actionRequired + '</td><td><a href="#" class="on-default showfile-btn" title="Download" data-id="' + result.attachment + '" style="margin-left:5px;"><i class="fa fa-file"></i></a></td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var date = new Date(result.date);
                    var dateofEmployement = new Date(result.dateofEmployement);

                    var pin = '<input id="staffreference-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffreferenceedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffReference/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffReference/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffReference/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffReference/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + date.toLocaleDateString() + '</td><td>' + dateofEmployement.toLocaleDateString() + '</td><td>' + result.contact + '</td><td>' + result.email + '</td><td>' + result.address + '</td><td>' + result.rehireStaff + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var pin = '<input id="staffpersonalitytest-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffpersonalitytestedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffPersonalityTest/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffPersonalityTest/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffPersonalityTest/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffPersonalityTest/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + result.questionName + '</td><td>' + result.answerName + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var testDate = new Date(result.testDate);

                    var pin = '<input id="infectioncontrol-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="infectioncontroledit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/InfectionControl/Print?clientId=' + result.infectionId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/InfectionControl/Email?clientId=' + result.infectionId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/InfectionControl/Download?clientId=' + result.infectionId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/InfectionControl/Delete?clientId=' + result.infectionId + '">Delete</a>';
                    var row = '<tr><td>' + testDate.toLocaleDateString() + '</td><td>' + result.guideline + '</td><td>' + result.remarks + '</td><td>' + result.infectionName + '</td><td>' + result.vaccName + '</td><td>' + result.typeName + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var pin = '<input id="staffcompetencetest-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffcompetencetestedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffCompetenceTest/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffCompetenceTest/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffCompetenceTest/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffCompetenceTest/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var pin = '<input id="healthcondition-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="healthconditionedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/HealthCondition/Print?Id=' + result.healthCondId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/HealthCondition/Email?Id=' + result.healthCondId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/HealthCondition/Download?Id=' + result.healthCondId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/HealthCondition/Delete?Id=' + result.healthCondId + '">Delete</a>';
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td><td>' + result.typeName + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var pin = '<input id="staffinterview-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffinterviewedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffInterview/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffInterview/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffInterview/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffInterview/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var pin = '<input id="staffshadowing-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="staffshadowingedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/StaffShadowing/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/StaffShadowing/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/StaffShadowing/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/StaffShadowing/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + result.titleName + '</td><td>' + result.answerName + '</td><td>' + result.comment + '</td><td>' + result.point + '</td><td>' + result.score + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                var startDate = new Date(result.startDate);
                var endDate = new Date(result.endDate);

                var pin = '<input id="staffholiday-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                var edit = '<a id="staffholidayedit" class="btn btn-warning" href="#">Edit</a>';
                var print = '<a class="btn btn-secondary" href="/StaffHoliday/Print?clientId=' + clientId + '">Print</a>';
                var email = '<a class="btn btn-success" href="/StaffHoliday/Email?clientId=' + clientId + '">Email</a>';
                var download = '<a class="btn btn-info" href="/StaffHoliday/Download?clientId=' + clientId + '">Download</a>';
                var del = '<a class="btn btn-danger" href="/StaffHoliday/Delete?clientId=' + clientId + '">Delete</a>';
                var row = '<tr><td>' + result.staffName + '</td><td>' + result.className + '</td><td>' + startDate.toLocaleDateString() + '</td><td>' + endDate.toLocaleDateString() + '</td><td>' + result.allocatedDays + '</td><td>' + result.balance + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var startDate = new Date(result.startDate);
                    var endDate = new Date(result.endDate);

                    var pin = '<input id="salaryallowance-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="salaryallowanceedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/SalaryAllowance/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/SalaryAllowance/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/SalaryAllowance/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/SalaryAllowance/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + startDate.toLocaleDateString() + '</td><td>' + endDate.toLocaleDateString() + '</td><td>' + result.percentage + '</td><td>' + result.amount + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
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
                    var startDate = new Date(result.startDate);
                    var endDate = new Date(result.endDate);

                    var pin = '<input id="salarydeduction-' + clientId + '" type="password" placeholder="Enter Pin" class="dropdown-item" onblur="checkPIN(this)" />';
                    var edit = '<a id="salarydeductionedit" class="btn btn-warning" href="#">Edit</a>';
                    var print = '<a class="btn btn-secondary" href="/SalaryDeduction/Print?clientId=' + clientId + '">Print</a>';
                    var email = '<a class="btn btn-success" href="/SalaryDeduction/Email?clientId=' + clientId + '">Email</a>';
                    var download = '<a class="btn btn-info" href="/SalaryDeduction/Download?clientId=' + clientId + '">Download</a>';
                    var del = '<a class="btn btn-danger" href="/SalaryDeduction/Delete?clientId=' + clientId + '">Delete</a>';
                    var row = '<tr><td>' + startDate.toLocaleDateString() + '</td><td>' + endDate.toLocaleDateString() + '</td><td>' + result.percentage + '</td><td>' + result.amount + '</td><td>' + pin + edit + print + email + download + del + '</td></tr>';
                    $('#tbl_' + name).append(row);

                });
            },
            error: function () {
                console.log('Failed ');
            }
        });
    }
}
