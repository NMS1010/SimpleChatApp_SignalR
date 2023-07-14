import { useState } from 'react';
import config from '../../configs';
import { Link } from 'react-router-dom';

const Register = () => {
    const [userInfo, setUserInfo] = useState({
        username: '',
        password: '',
        confirmPassword: '',
        dateOfBirth: new Date(),
        email: '',
        phoneNumber: '',
        firstName: '',
        lastName: '',
        gender: 'Nam',
    });
    const onChange = (e) => {
        const { name, value } = e.target;
        setUserInfo({ [name]: value });
    };
    return (
        <section className="border-red-500 bg-gray-200 min-h-screen flex items-center justify-center">
            <div className="bg-gray-100 p-5 flex rounded-2xl shadow-lg max-w-4xl">
                <div className="px-5">
                    <h2 className="text-2xl font-bold text-[#002D74]">Register</h2>
                    <form className="mt-6" action="#" method="POST">
                        <div className="mt-4 flex">
                            <div className="mr-2">
                                <input
                                    type="text"
                                    name="username"
                                    id="username"
                                    placeholder="Username"
                                    className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                    autoFocus
                                    onChange={onChange}
                                    value={userInfo.username}
                                    required
                                />
                            </div>
                            <div className="ml-2">
                                <input
                                    type="number"
                                    name="phoneNumber"
                                    id="phoneNumber"
                                    placeholder="Phone"
                                    className="[appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                    autoFocus
                                    onChange={onChange}
                                    value={userInfo.phoneNumber}
                                    required
                                />
                            </div>
                        </div>
                        <div className="mt-4">
                            <input
                                type="email"
                                name="email"
                                id="email"
                                placeholder="Email"
                                className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                autoFocus
                                onChange={onChange}
                                value={userInfo.email}
                                required
                            />
                        </div>
                        <div className="mt-4 flex">
                            <div className="mr-2">
                                <input
                                    type="text"
                                    name="firstName"
                                    id="firstName"
                                    placeholder="First name"
                                    className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                    autoFocus
                                    onChange={onChange}
                                    value={userInfo.firstName}
                                    required
                                />
                            </div>
                            <div className="ml-2">
                                <input
                                    type="text"
                                    name="lastName"
                                    id="lastName"
                                    placeholder="Last name"
                                    className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                    autoFocus
                                    onChange={onChange}
                                    value={userInfo.lastName}
                                    required
                                />
                            </div>
                        </div>
                        <div className="mt-4 flex items-center">
                            <div className="mr-2">
                                <input
                                    type="date"
                                    name="dateOfBirth"
                                    id="dateOfBirth"
                                    className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                    autoFocus
                                    onChange={onChange}
                                    value={userInfo.firstName}
                                    required
                                />
                            </div>
                            <div className="ml-2 grow">
                                <select
                                    name="gender"
                                    className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                    id="gender"
                                >
                                    <option value="1">Nam</option>
                                    <option value="2">Nữ</option>
                                    <option value="3">Khác</option>
                                </select>
                            </div>
                        </div>
                        <div className="mt-4">
                            <input
                                type="password"
                                name="password"
                                id="password"
                                placeholder="Password"
                                onChange={onChange}
                                value={userInfo.password}
                                className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                required
                            />
                        </div>

                        <div className="mt-4">
                            <input
                                type="password"
                                name="confirmPassword"
                                id="confirmPassword"
                                placeholder="Confirm password"
                                onChange={onChange}
                                value={userInfo.confirmPassword}
                                className="w-full px-4 py-3 rounded-lg bg-gray-200 mt-2 border focus:border-blue-500 focus:bg-white focus:outline-none"
                                required
                            />
                        </div>

                        <button
                            type="submit"
                            className="w-full block bg-blue-500 hover:bg-blue-400 focus:bg-blue-400 text-white font-semibold rounded-lg  px-4 py-3 mt-6"
                        >
                            Register
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
                        <p className="text-sm mt-4 text-[#002D74]">If you have an account, please Login</p>
                        <Link
                            to={config.routes.login}
                            className="py-2 px-5 ml-3 bg-white border rounded-xl hover:scale-110 duration-300 border-blue-400  "
                        >
                            Login
                        </Link>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default Register;
