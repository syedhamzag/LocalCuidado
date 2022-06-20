var reciveraudio = document.getElementById("ReceiverId")?.value;
var senderaudio = document.getElementById("SenderId")?.value;


var peerAudio = new Peer("audio" + senderaudio);
var myStreamAudio;
var dialedcallAudio;

function AudioCallReciver() {


    var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;
    getUserMedia({ video: false, audio: true }, function (stream) {
        var call = peerAudio.call("audio" + reciveraudio, stream);
        myStreamAudio = stream;
        const myvideo = document.getElementById("myaudio");
        console.log(stream);
        myvideo.srcObject = stream;
        myvideo.play();
        dialedcallAudio = call;
        call.on('stream', function (remoteStream) {
            const video = document.getElementById("useraudio");
            console.log(remoteStream);
            video.srcObject = remoteStream;
            video.play();
            dialedcallAudio.peerConnection.onconnectionstatechange = function (state) {
                if (state.target.iceConnectionState === "disconnected") {
                    myStreamAudio.getTracks().forEach(function (track) {
                        track.stop();
                    });
                    DisconnectAudio();
                }

            }
        });
    }, function (err) {
        console.log('Failed to get local stream', err);
    });
}


function muteMicAudio() {
    myStreamAudio.getAudioTracks().forEach(track => track.enabled = !track.enabled);
}

function muteCamAudio() {
    myStreamAudio.getVideoTracks().forEach(track => track.enabled = !track.enabled);
}
var upcommingAudiocall;
var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

peerAudio.on('call', function (call) {

    upcommingAudiocall = call;
    $("#answeraudiocallmodel").modal("show");

});


function answerAudioCall() {

    var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;
    document.getElementById("AnswerDeclineDivaudio").style.display = "none";
    document.getElementById("videoReciveDivaudio").style.display = "block";
    getUserMedia({ video: false, audio: true }, function (stream) {
        upcommingAudiocall.answer(stream); // Answer the call with an A/V stream.
        const myvideo = document.getElementById("myaudio");
        console.log(stream);
        myStreamAudio = stream;
        myvideo.srcObject = stream;
        myvideo.play();
        upcommingAudiocall.on('stream', function (remoteStream) {
            const video = document.getElementById("useraudio");
            console.log(remoteStream)
            video.srcObject = remoteStream;
            //video.play();
            upcommingAudiocall.peerConnection.onconnectionstatechange = function (state) {
                if (state.target.iceConnectionState === "disconnected") {
                    myStreamAudio.getTracks().forEach(function (track) {
                        track.stop();
                    });
                    declineCallAudio();
                }

            }
        });

    }, function (err) {
        console.log('Failed to get local stream', err);
    });
}
function DisconnectAudio() {
    dialedcallAudio.close();
    peerAudio.disconnect();
    //peer.destroy()
    history.back();
}
function declineCallAudio() {
    upcommingAudiocall.close();
    if (myStreamAudio !== null && myStreamAudio !== undefined) {
        myStreamAudio?.getTracks()?.forEach(function (track) {
            track.stop();
        });
    }
    $("#answeraudiocallmodel").modal("hide");
    document.getElementById("AnswerDeclineDivaudio").style.display = "block";
    document.getElementById("videoReciveDivaudio").style.display = "none";
}

async function DeclineAfterAnser() {
    var a = await AnwserWithPromise();
    declineCallAudio();

}
const createEmptyAudioTrack = () => {
    const ctx = new AudioContext();
    const oscillator = ctx.createOscillator();
    const dst = oscillator.connect(ctx.createMediaStreamDestination());
    oscillator.start();
    const track = dst.stream.getAudioTracks()[0];
    return Object.assign(track, { enabled: false });
};
const createEmptyVideoTrack = ({ width, height }) => {
    const canvas = Object.assign(document.createElement('canvas'), { width, height });
    canvas.getContext('2d').fillRect(0, 0, width, height);

    const stream = canvas.captureStream();
    const track = stream.getVideoTracks()[0];

    return Object.assign(track, { enabled: false });
};

function AnwserWithPromise() {
    return new Promise((resolve, reject) => {
        const audioTrack = createEmptyAudioTrack();
        const videoTrack = createEmptyVideoTrack({ width: 100, height:100 });
        const amediaStream = new MediaStream([audioTrack, videoTrack]);
        upcommingAudiocall.answer(amediaStream); // Answer the call with an A/V stream.
            const myvideo = document.getElementById("myaudio");
        console.log(amediaStream);
        myStreamAudio = amediaStream;
        myvideo.srcObject = amediaStream;
            myvideo.play();
            upcommingAudiocall.on('stream', function (remoteStream) {
                const video = document.getElementById("useraudio");
                console.log(remoteStream)
                video.srcObject = remoteStream;
                //video.play();
                upcommingAudiocall.peerConnection.onconnectionstatechange = function (state) {
                    if (state.target.iceConnectionState === "disconnected") {
                        myStreamAudio.getTracks().forEach(function (track) {
                            track.stop();
                        });
                        declineCallAudio();
                    }
                    if (state.target.iceConnectionState === "connected") {
                        resolve();
                    }

                }
               
            });

       

    });
}

