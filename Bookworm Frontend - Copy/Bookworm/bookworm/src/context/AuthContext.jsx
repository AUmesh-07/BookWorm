import React, { useState, useEffect, createContext, useContext, useMemo } from 'react';
import axios from 'axios';

// 1. AXIOS INSTANCE
const axiosInstance = axios.create({
  baseURL: 'https://localhost:7261/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// 2. CONTEXT SETUP
const AuthContext = createContext();

// 3. PROVIDER COMPONENT
export function AuthProvider({ children }) {
  const [auth, setAuth] = useState({ token: null, user: null });
  const [isAuthReady, setIsAuthReady] = useState(false);

  // 4. LOAD AUTH DATA FROM LOCAL STORAGE ON INITIAL LOAD
  useEffect(() => {
    const token = localStorage.getItem("token");
    const userStr = localStorage.getItem("user");

    if (token && userStr && userStr !== "undefined") {
      try {
        const user = JSON.parse(userStr);
        setAuth({ token, user });
      } catch (error) {
        console.error("Failed to parse user data from localStorage", error);
        localStorage.removeItem('user');
        localStorage.removeItem('token');
      }
    }
    setIsAuthReady(true);
  }, []);

  // 5. AXIOS INTERCEPTOR TO ATTACH TOKEN TO REQUESTS
  useEffect(() => {
    const requestInterceptor = axiosInstance.interceptors.request.use(
      (config) => {
        if (auth.token) {
          config.headers['Authorization'] = `Bearer ${auth.token}`;
        }
        return config;
      },
      (error) => Promise.reject(error)
    );

    return () => {
      axiosInstance.interceptors.request.eject(requestInterceptor);
    };
  }, [auth.token]);

  // 6. LOGIN FUNCTION
  const login = async (email, password) => {
    try {
      const response = await axiosInstance.post('/auth/login', { email, password });
      const { token, id, name, type } = response.data;
      
      if (token && id && name) {
        const user = { id, name, type };
        localStorage.setItem('token', token);
        localStorage.setItem('user', JSON.stringify(user));
        setAuth({ token, user });
        return true;
      } else {
        console.error("Login response missing expected fields.");
        return false;
      }
    } catch (error) {
      console.error('Login failed:', error.response?.data?.message || error.message);
      return false;
    }
  };

  // 7. LOGOUT FUNCTION
  const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    setAuth({ token: null, user: null });
  };

  // 8. CONTEXT VALUE
  const contextValue = useMemo(() => ({
    auth,
    isAuthenticated: !!auth.token,
    isAuthReady,
    login,
    logout,
  }), [auth, isAuthReady]);

  return (
    <AuthContext.Provider value={contextValue}>
      {isAuthReady ? children : <div>Loading...</div>}
    </AuthContext.Provider>
  );
}

// 9. CUSTOM HOOK
export function useAuth() {
  return useContext(AuthContext);
}
