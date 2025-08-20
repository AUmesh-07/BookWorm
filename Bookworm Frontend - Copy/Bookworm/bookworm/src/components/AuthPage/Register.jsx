// import React, { useState } from 'react';
// import { Link, useNavigate } from 'react-router-dom';
// import axios from 'axios'; // You'll need to install axios: npm install axios
// import { useAuth } from '../../context/AuthContext';

// // This instance should ideally be in a central api file, but for simplicity here it is.
// const axiosInstance = axios.create({
//   baseURL: 'http://localhost:8080/api', // Your backend API base URL
// });

// function RegisterPage() {
//   const navigate = useNavigate();
//   const [form, setForm] = useState({ name: "", email: "", password: "", confirmPassword: "" });
//   const [error, setError] = useState('');
//   const [isLoading, setIsLoading] = useState(false);

//   const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     setError('');
//     if (form.password !== form.confirmPassword) {
//       setError("Passwords do not match");
//       return;
//     }
//     setIsLoading(true);
//     try {
//       await axiosInstance.post('/auth/register', {
//         name: form.name,
//         email: form.email,
//         password: form.password,
//       });
//       // On success, redirect to the login page
//       navigate('/login', { state: { message: 'Registration successful! Please log in.' } });
//     } catch (err) {
//       setError(err.response?.data?.message || "Registration failed. Please try again.");
//     } finally {
//       setIsLoading(false);
//     }
//   };

//   return (
//     <div className="container d-flex justify-content-center align-items-center min-vh-100">
//       <div className="card shadow p-4" style={{ maxWidth: "500px", width: "100%" }}>
//         <div className="text-center mb-4">
//           <div style={{ fontSize: "40px", color: "#e91e63" }}>📖</div>
//           <h2 className="fw-bold">Create your <span style={{color: "#e91e63"}}>Bookworm</span> account</h2>
//         </div>
//         <form onSubmit={handleSubmit}>
//           {error && <div className="alert alert-danger">{error}</div>}
//           <div className="mb-3 text-start"><label className="form-label">Full Name</label><input type="text" className="form-control" name="name" placeholder="John Doe" required onChange={handleChange} /></div>
//           <div className="mb-3 text-start"><label className="form-label">Email</label><input type="email" className="form-control" name="email" placeholder="name@example.com" required onChange={handleChange} /></div>
//           <div className="mb-3 text-start"><label className="form-label">Password</label><input type="password" className="form-control" name="password" placeholder="Create a password" required onChange={handleChange} /></div>
//           <div className="mb-3 text-start"><label className="form-label">Confirm Password</label><input type="password" className="form-control" name="confirmPassword" placeholder="Confirm your password" required onChange={handleChange} /></div>
//           <button type="submit" className="btn w-100" style={{backgroundColor: "#e91e63", color: "white"}} disabled={isLoading}>{isLoading ? 'Creating Account...' : 'Create Account'}</button>
//         </form>
//         <div className="text-center mt-3"><span className="text-muted">Already have an account? </span><Link to="/login" style={{color: "#e91e63"}}>Sign in</Link></div>
//       </div>
//     </div>
//   );
// }

// export default RegisterPage;