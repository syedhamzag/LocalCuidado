var reciver = document.getElementById("ReceiverId")?.value;
var sender = document.getElementById("SenderId")?.value;
var reciverVideo = document.getElementById("uservideo");
var senderVideo = document.getElementById("myvideo");


var peer = new Peer(sender);
var myStream;
var dialedcall;
function VideoCallReciver() {

    var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;
    getUserMedia({ video: true, audio: true }, function (stream) {
        var call = peer.call(reciver, stream);
        myStream = stream;
        senderVideo.srcObject = stream;
        senderVideo.play();
        dialedcall = call;
        call.on('stream', function (remoteStream) {
            reciverVideo.srcObject = remoteStream;
            reciverVideo.play();
            dialedcall.peerConnection.onconnectionstatechange = function (state) {
                if (state.target.iceConnectionState === "disconnected") {
                    myStream.getTracks().forEach(function (track) {
                        track.stop();
                    });
                    Disconnect();
                }

            }
        });
    }, function (err) {
        console.log('Failed to get local stream', err);
    });
}

function muteMic() {
    myStream.getAudioTracks().forEach(track => track.enabled = !track.enabled);
}

function muteCam() {
    myStream.getVideoTracks().forEach(track => track.enabled = !track.enabled);
}
var upcommingcall;
var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

peer.on('call', function (call) {

    upcommingcall = call;
    console.log(call);
    $("#answercallmodel").modal("show");

});


function answerCall() {

    var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;
    document.getElementById("AnswerDeclineDiv").style.display = "none";
    document.getElementById("videoReciveDiv").style.display = "block";
    getUserMedia({ video: true, audio: true }, function (stream) {
        upcommingcall.answer(stream); // Answer the call with an A/V stream.
        myStream = stream;
        senderVideo.srcObject = stream;
        senderVideo.play();
        upcommingcall.on('stream', function (remoteStream) {
            reciverVideo.srcObject = remoteStream;
            //reciverVideo.play();
            upcommingcall.peerConnection.onconnectionstatechange = function (state) {
                if (state.target.iceConnectionState === "disconnected") {
                    myStream.getTracks().forEach(function (track) {
                        track.stop();
                    });
                    declineCall();
                }

            }
        });

    }, function (err) {
        console.log('Failed to get local stream', err);
    });
}
function Disconnect() {
    dialedcall.close();
    peer.disconnect();
    //peer.destroy()
    history.back();
}
function declineCall() {
    upcommingcall.close();
    myStream.getTracks().forEach(function (track) {
        track.stop();
    });
    $("#answercallmodel").modal("hide");
    document.getElementById("AnswerDeclineDiv").style.display = "block";
    document.getElementById("videoReciveDiv").style.display = "none";
}
