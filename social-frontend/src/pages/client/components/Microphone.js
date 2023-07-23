import { useEffect, useRef, useState } from 'react';
import Swal from 'sweetalert2';

const Microphone = ({ setAudio, setRecordingStatus, stopRecording, dNone }) => {
    const mimeType = 'audio/webm';
    const [permission, setPermission] = useState(false);
    const [stream, setStream] = useState(null);
    const mediaRecorder = useRef(null);
    const [audioChunks, setAudioChunks] = useState([]);

    useEffect(() => {
        if (stopRecording) {
            stopRecord();
        }
    }, [stopRecording]);
    const getMicroPermission = async () => {
        const streamData = await navigator.mediaDevices.getUserMedia({
            audio: true,
        });
        setStream(streamData);
        setPermission(true);
    };
    const stopRecord = () => {
        if (!mediaRecorder?.current) return;
        // setRecordingStatus('inactive');
        //stops the recording instance
        mediaRecorder.current.stop();
        mediaRecorder.current.onstop = () => {
            //creates a blob file from the audiochunks data
            const audioBlob = new Blob(audioChunks, { type: mimeType });
            //creates a playable URL from the blob file.
            const audioUrl = URL.createObjectURL(audioBlob);
            setAudio(audioUrl);
            setAudioChunks([]);
        };
    };
    const startRecording = async () => {
        setRecordingStatus('recording');
        //create new Media recorder instance using the stream
        const media = new MediaRecorder(stream, { type: mimeType });
        //set the MediaRecorder instance to the mediaRecorder ref
        mediaRecorder.current = media;
        //invokes the start method to start the recording process
        mediaRecorder.current.start();

        let localAudioChunks = [];
        mediaRecorder.current.ondataavailable = (event) => {
            if (typeof event.data === 'undefined') return;
            if (event.data.size === 0) return;
            localAudioChunks.push(event.data);
        };
        setAudioChunks(localAudioChunks);
    };
    const onRecord = () => {
        navigator.permissions
            .query({ name: 'microphone' })
            .then(async (result) => {
                await getMicroPermission();
                await startRecording();
            })
            .catch((e) => {
                console.log(e);
                setRecordingStatus('inactive');
                setPermission(false);
                Swal.fire({
                    title: 'Failed to get microphone access',
                    text: 'Please allow to access your microphone',
                    icon: 'error',
                    allowOutsideClick: false,
                }).then((result) => {
                    if (result.isConfirmed) {
                        Swal.close();
                    }
                });
            });
    };
    return (
        <span style={{display: `${dNone ? 'none' : ''}`}} className="absolute inset-y-0 flex items-center">
            <button
                onClick={onRecord}
                type="button"
                className="inline-flex items-center justify-center rounded-full h-12 w-12 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300 focus:outline-none"
            >
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                    className="h-6 w-6 text-gray-600"
                >
                    <path
                        stroke-linecap="round"
                        stroke-linejoin="round"
                        stroke-width="2"
                        d="M19 11a7 7 0 01-7 7m0 0a7 7 0 01-7-7m7 7v4m0 0H8m4 0h4m-4-8a3 3 0 01-3-3V5a3 3 0 116 0v6a3 3 0 01-3 3z"
                    ></path>
                </svg>
            </button>
        </span>
    );
};
export default Microphone;
