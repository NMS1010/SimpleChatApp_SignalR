import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import config from '../../configs';
import loginBg from '../../assets/images/login-bg.png';
import * as authAction from '../../redux/features/auth/authSlice';
import Swal from 'sweetalert2';
import { useDispatch } from 'react-redux';
import Loading from '../../components/Loading';

const Login = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [userInfo, setUserInfo] = useState({
        username: '',
        password: '',
    });
    const [validateUserInfo, setValidateUserInfo] = useState({
        username: '',
        password: '',
    });
    const [loading, setLoading] = useState(false);

    const onChange = (e) => {
        const { name, value } = e.target;
        let msg = 'This field is required!';
        if (value) msg = '';
        setValidateUserInfo({ ...validateUserInfo, [name]: msg });
        setUserInfo({ ...userInfo, [name]: value });
    };
    const onSubmit = async () => {
        setLoading(true);
        const resp = await dispatch(authAction.login({ username: userInfo.username, password: userInfo.password }));
        setLoading(false);
        let icon = 'success';
        let text = 'Login successfully';
        if (!resp.payload) {
            Swal.fire({
                title: 'Failed to Login',
                text: 'Cannot connect to server',
                icon: 'error',
                allowOutsideClick: false,
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.close();
                }
            });
            return;
        }
        let { isSuccess, errors } = resp.payload;
        if (isSuccess === undefined) {
            isSuccess = resp.payload.data.isSuccess;
            errors = resp.payload.data.errors;
        }
        if (isSuccess === false) {
            icon = 'error';
            text = errors?.join('\n');
        }
        Swal.fire({
            title: text,
            icon: icon,
            allowOutsideClick: false,
        }).then((result) => {
            if (result.isConfirmed) {
                if (isSuccess) {
                    navigate('/chat');
                }
            }
        });
    };
    return loading ? (
        <Loading />
    ) : (
        <section className="border-red-500 bg-gray-200 min-h-screen flex items-center justify-center">
            <div className="bg-gray-100 p-5 flex rounded-2xl shadow-lg max-w-4xl">
                <div className="md:w-1/2 px-5">
                    <h2 className="text-2xl font-bold text-[#002D74]">Login</h2>
                    <p className="text-sm mt-4 text-[#002D74]">If you have an account, please login</p>
                    <form className="mt-6" action="#" method="POST">
                        <div>
                            <input
                                type="text"
                                name="username"
                                id="username"
                                placeholder="Username"
                                className={`w-full ${
                                    validateUserInfo.username && 'border-red-500'
                                } px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none`}
                                autoFocus
                                onChange={onChange}
                                value={userInfo.username}
                                required
                            />
                            <p className="text-left text-red-500 ml-2 mt-2">{validateUserInfo.username}</p>
                        </div>

                        <div className="mt-4">
                            <input
                                type="password"
                                name="password"
                                id="password"
                                placeholder="Password"
                                onChange={onChange}
                                value={userInfo.password}
                                className={`w-full ${
                                    validateUserInfo.password && 'border-red-500'
                                } px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none`}
                                required
                            />
                            <p className="text-left text-red-500 ml-2 mt-2">{validateUserInfo.password}</p>
                        </div>

                        <div className="text-right mt-2">
                            <Link
                                to={'#'}
                                className="text-sm font-semibold text-gray-700 hover:text-blue-700 focus:text-blue-700"
                            >
                                Forgot Password?
                            </Link>
                        </div>

                        <button
                            onClick={onSubmit}
                            type="button"
                            disabled={validateUserInfo.username || validateUserInfo.password}
                            className={`w-full block ${
                                !validateUserInfo.username && !validateUserInfo.password
                                    ? 'bg-blue-500 hover:bg-blue-400 focus:bg-blue-400'
                                    : 'bg-gray-500 hover:bg-gray-500 focus:bg-gray-500'
                            }  text-white font-semibold rounded-lg  px-4 py-3 mt-6`}
                        >
                            Log In
                        </button>
                    </form>

                    <div className="mt-7 grid grid-cols-3 items-center text-gray-500">
                        <hr className="border-gray-500" />
                        <p className="text-center text-sm">OR</p>
                        <hr className="border-gray-500" />
                    </div>

                    <button className="bg-white border py-2 w-full rounded-xl mt-5 flex justify-center items-center text-sm hover:scale-105 duration-300 ">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            xmlnsXlink="http://www.w3.org/1999/xlink"
                            className="w-6 h-6"
                            viewBox="0 0 48 48"
                        >
                            <defs>
                                <path
                                    id="a"
                                    d="M44.5 20H24v8.5h11.8C34.7 33.9 30.1 37 24 37c-7.2 0-13-5.8-13-13s5.8-13 13-13c3.1 0 5.9 1.1 8.1 2.9l6.4-6.4C34.6 4.1 29.6 2 24 2 11.8 2 2 11.8 2 24s9.8 22 22 22c11 0 21-8 21-22 0-1.3-.2-2.7-.5-4z"
                                />
                            </defs>
                            <clipPath id="b">
                                <use xlinkHref="#a" overflow="visible" />
                            </clipPath>
                            <path clipPath="url(#b)" fill="#FBBC05" d="M0 37V11l17 13z" />
                            <path clipPath="url(#b)" fill="#EA4335" d="M0 11l17 13 7-6.1L48 14V0H0z" />
                            <path clipPath="url(#b)" fill="#34A853" d="M0 37l30-23 7.9 1L48 0v48H0z" />
                            <path clipPath="url(#b)" fill="#4285F4" d="M48 48L17 24l-4-3 35-10z" />
                        </svg>
                        <span className="ml-4">Login with Google</span>
                    </button>

                    <div className="text-sm flex justify-between items-center mt-3">
                        <p>If you don't have an account...</p>
                        <Link
                            to={config.routes.register}
                            className="py-2 px-5 ml-3 bg-white border rounded-xl hover:scale-110 duration-300 border-blue-400  "
                        >
                            Register
                        </Link>
                    </div>
                </div>

                <div className="w-1/2 md:flex items-center ">
                    <img src={loginBg} className="rounded-2xl" alt="page img" />
                </div>
            </div>
        </section>
    );
};

export default Login;
