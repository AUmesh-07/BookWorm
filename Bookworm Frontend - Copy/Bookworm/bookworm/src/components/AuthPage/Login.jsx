import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../../context/AuthContext';
import './Login.css'; // Make sure the CSS file is named Login.css or update this import

// This should ideally be in a central api file, but is here for simplicity.
const axiosInstance = axios.create({
  baseURL: 'https://localhost:7261/api',
});

function Login() {
  const { login } = useAuth();
  const navigate = useNavigate();

  // State to control the sliding panel animation
  const [isPanelActive, setIsPanelActive] = useState(false);

  // State for forms
  const [loginForm, setLoginForm] = useState({ email: '', password: '' });
  const [registerForm, setRegisterForm] = useState({ name: '', email: '', password: '' });

  // State for errors and loading
  const [loginError, setLoginError] = useState('');
  const [registerError, setRegisterError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  // Handlers for form input changes
  const handleLoginChange = (e) => setLoginForm({ ...loginForm, [e.target.name]: e.target.value });
  const handleRegisterChange = (e) => setRegisterForm({ ...registerForm, [e.target.name]: e.target.value });

  // Handler for login submission
  const handleLoginSubmit = async (e) => {
    e.preventDefault();
    setLoginError('');
    setIsLoading(true);
    try {
      const success = await login(loginForm.email, loginForm.password);
      if (success) {
        navigate('/'); // Navigate to home on successful login
      } else {
        setLoginError('Login failed. Please check your credentials.');
      }
    } catch (err) {
      setLoginError('An unexpected error occurred.');
    } finally {
      setIsLoading(false);
    }
  };

  // Handler for registration submission
  const handleRegisterSubmit = async (e) => {
    e.preventDefault();
    setRegisterError('');
    // Basic validation for password confirmation can be added here if needed
    setIsLoading(true);
    try {
      await axiosInstance.post('/auth/register', registerForm);
      // On success, switch back to the sign-in panel
      setIsPanelActive(false);
      setLoginError('Registration successful! Please sign in.'); // Give user feedback
    } catch (err) {
      setRegisterError(err.response?.data?.message || 'Registration failed.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="auth-container d-flex justify-content-center align-items-center min-vh-100">
      <div className={`auth-panel-container rounded-4 shadow-lg ${isPanelActive ? 'right-panel-active' : ''}`} id="container">
        {/* Sign Up Form */}
        <div className="form-container sign-up-container">
          <form onSubmit={handleRegisterSubmit} className="auth-form d-flex flex-column h-100 justify-content-center">
            <h1 className="fw-bold mb-4">Create Account</h1>
            {registerError && <div className="alert alert-danger p-2 small">{registerError}</div>}
            <input type="text" name="name" placeholder="Name" required value={registerForm.name} onChange={handleRegisterChange} className="form-control" />
            <input type="email" name="email" placeholder="Email" required value={registerForm.email} onChange={handleRegisterChange} className="form-control" />
            <input type="password" name="password" placeholder="Password" required value={registerForm.password} onChange={handleRegisterChange} className="form-control" />
            <button type="submit" className="btn auth-button mt-3" disabled={isLoading}>
              {isLoading ? 'Signing Up...' : 'Sign Up'}
            </button>
          </form>
        </div>

        {/* Sign In Form */}
        <div className="form-container sign-in-container">
          <form onSubmit={handleLoginSubmit} className="auth-form d-flex flex-column h-100 justify-content-center">
            <h1 className="fw-bold mb-4">Sign In</h1>
            {loginError && <div className="alert alert-danger p-2 small">{loginError}</div>}
            <input type="email" name="email" placeholder="Email" required value={loginForm.email} onChange={handleLoginChange} className="form-control" />
            <input type="password" name="password" placeholder="Password" required value={loginForm.password} onChange={handleLoginChange} className="form-control" />
            <a href="#">Forgot your password?</a>
            <button type="submit" className="btn auth-button mt-3" disabled={isLoading}>
              {isLoading ? 'Signing In...' : 'Sign In'}
            </button>
          </form>
        </div>

        {/* Overlay Panels */}
        <div className="overlay-container">
          <div className="overlay">
            <div className="overlay-panel overlay-left">
              <h1 className="fw-bold">Welcome Back!</h1>
              <p className="fs-5 mt-2">To keep connected with us please login with your personal info</p>
              <button className="btn auth-ghost-button mt-3" onClick={() => setIsPanelActive(false)}>
                Sign In
              </button>
            </div>
            <div className="overlay-panel overlay-right">
              <h1 className="fw-bold">Hello, Friend!</h1>
              <p className="fs-5 mt-2">Enter your personal details and start your journey with us</p>
              <button className="btn auth-ghost-button mt-3" onClick={() => setIsPanelActive(true)}>
                Sign Up
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login;
