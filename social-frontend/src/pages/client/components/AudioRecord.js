import { useEffect, useState } from 'react';
const MAX_RECORD_TIME = 60;
const sec2min = (second) => {
    const min = Math.floor(second / 60);
    const sec = second - min * 60;
    let temp = `${sec}`;
    if (sec < 10) temp = `0${sec}`;
    return `${min}:${temp}`;
};
const AudioRecord = ({ audio, setStopRecording, onCancel, stopRecording }) => {
    const [time, setTime] = useState(0);
    const [intervalId, setIntervalId] = useState(-1);
    const [audioPlayer, setAudioPlayer] = useState(null);
    const [playing, setPlaying] = useState(false);
    useEffect(() => {
        const id = setInterval(function () {
            setTime((time) => time + 1);
        }, 1000);
        setIntervalId(id);
        return () => {
            clearInterval(id);
        };
    }, []);
    useEffect(() => {
        if (stopRecording) {
            setPlaying(stopRecording);
        }
    }, [stopRecording]);
    useEffect(() => {
        if (time >= MAX_RECORD_TIME) {
            clearInterval(intervalId);
        }
    }, [time]);
    const onClick = () => {
        clearInterval(intervalId);
        if (!stopRecording) {
            setStopRecording(true);
            setTime(0);
        }
        setPlaying(!playing);
    };
    useEffect(() => {
        if (audioPlayer) {
            !playing ? audioPlayer.play() : audioPlayer.pause();
        }
    }, [playing]);

    useEffect(() => {
        if (audio) {
            let a = new Audio(audio);
            a.addEventListener('ended', () => setPlaying(true));
            a.addEventListener('timeupdate', (e) => {
                setTime(Math.floor(a.currentTime))
            })
            setAudioPlayer(a);
        }
        return () => {
            audioPlayer?.removeEventListener('ended');
        };
    }, [audio]);
    return (
        <div className="flex" style={{flexGrow: '0.2' }}>
            <span className="flex items-center mr-3 transform rotate-45">
                <div onClick={onCancel} ariaLabel="Đóng" role="button" tabindex="0">
                    <svg height="20px" viewBox="0 0 24 24" width="20px">
                        <g fillRule="evenodd">
                            <polygon fill="none" points="-6,30 30,30 30,-6 -6,-6 "></polygon>
                            <path d="m18,11l-5,0l0,-5c0,-0.552 -0.448,-1 -1,-1c-0.5525,0 -1,0.448 -1,1l0,5l-5,0c-0.5525,0 -1,0.448 -1,1c0,0.552 0.4475,1 1,1l5,0l0,5c0,0.552 0.4475,1 1,1c0.552,0 1,-0.448 1,-1l0,-5l5,0c0.552,0 1,-0.448 1,-1c0,-0.552 -0.448,-1 -1,-1m-6,13c-6.6275,0 -12,-5.3725 -12,-12c0,-6.6275 5.3725,-12 12,-12c6.627,0 12,5.3725 12,12c0,6.6275 -5.373,12 -12,12"></path>
                        </g>
                    </svg>
                    <div dataVisualcompletion="ignore"></div>
                </div>
            </span>
            <audio className="ml-16 bg-red-300" style={{ display: 'none' }} src={audio} controls></audio>
            <div className="flex flex-grow items-center cursor-pointer h-9 bg-purple-500 rounded-lg overflow-hidden ">
                <div className="flex">
                    <div className="" style={{ transform: 'translateX(-17.5%);' }}></div>
                    <div
                        ariaLabel="Thanh kéo âm thanh"
                        ariaValuemax="30.037333"
                        ariaValuemin="0"
                        ariaValuenow="24.783597"
                        role="slider"
                        tabindex="0"
                        className="absolute"
                    ></div>
                    <div className="" style={{ transform: 'translateX(-100%);' }}></div>
                    <div className="flex ">
                        <div className="rounded-full bg-white ml-3">
                            <div onClick={onClick} ariaLabel="Phát" className="" role="button" tabindex="0">
                                <svg aria-hidden="true" height="24px" viewBox="0 0 36 36" width="24px">
                                    {playing ? (
                                        // play button
                                        <path
                                            d="M10 25.5v-15a1.5 1.5 0 012.17-1.34l15 7.5a1.5 1.5 0 010 2.68l-15 7.5A1.5 1.5 0 0110 25.5z"
                                            fill="#7646ff"
                                        ></path>
                                    ) : (
                                        // pause button
                                        <path
                                            d="M11 8.5c-.83 0-1.5.67-1.5 1.5v16c0 .83.67 1.5 1.5 1.5h4c.83 0 1.5-.67 1.5-1.5V10c0-.83-.67-1.5-1.5-1.5h-4zM21 8.5c-.83 0-1.5.67-1.5 1.5v16c0 .83.67 1.5 1.5 1.5h4c.83 0 1.5-.67 1.5-1.5V10c0-.83-.67-1.5-1.5-1.5h-4z"
                                            fill="#7646ff"
                                        ></path>
                                    )}
                                </svg>
                                <div className="" dataVisualcompletion="ignore"></div>
                            </div>
                        </div>
                        <div className="cursor-pointer ml-3 text-white">{sec2min(time)}</div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AudioRecord;
