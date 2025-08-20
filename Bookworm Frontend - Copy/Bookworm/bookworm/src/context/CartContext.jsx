import React, { createContext, useState, useContext, useEffect } from 'react';
import axios from 'axios';

const CartContext = createContext();

export const useCart = () => useContext(CartContext);

export const CartProvider = ({ children }) => {
    const [cartCount, setCartCount] = useState(0);

    const axiosInstance = axios.create({
        baseURL: 'https://localhost:7261/api/v1',
    });

    axiosInstance.interceptors.request.use(
        (config) => {
            const token = localStorage.getItem('token');
            if (token) {
                config.headers['Authorization'] = `Bearer ${token}`;
            }
            return config;
        },
        (error) => Promise.reject(error)
    );

    // ✅ FIX: This function now gets the full cart and sets the count
    const fetchCartCount = async () => {
        const userStr = localStorage.getItem('user');
        const user = userStr ? JSON.parse(userStr) : null;

        if (user && user.id) {
            try {
                const response = await axiosInstance.get(`/carts/${user.id}`);
                // Check if the response has an 'items' array
                if (response.data && Array.isArray(response.data.items)) {
                    setCartCount(response.data.items.length);
                } else {
                    setCartCount(0);
                }
            } catch (err) {
                console.error("Failed to fetch cart data:", err);
                setCartCount(0);
            }
        } else {
            setCartCount(0);
        }
    };

    useEffect(() => {
        fetchCartCount();
    }, []);

    const incrementCartCount = (count = 1) => {
        setCartCount(prevCount => prevCount + count);
    };

    const decrementCartCount = (count = 1) => {
        setCartCount(prevCount => Math.max(0, prevCount - count));
    };

    const resetCartCount = () => {
        setCartCount(0);
    };

    return (
        <CartContext.Provider value={{ cartCount, fetchCartCount, incrementCartCount, decrementCartCount, resetCartCount }}>
            {children}
        </CartContext.Provider>
    );
};