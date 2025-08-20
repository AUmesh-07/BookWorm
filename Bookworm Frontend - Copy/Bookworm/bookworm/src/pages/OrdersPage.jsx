import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { toast, ToastContainer } from 'react-toastify';
import axios from 'axios';
import Footer from '../components/Footer';
import OrderCard from '../components/Card/OrderCard'; // We will create this component next
import './OrdersPage.css';

const axiosInstance = axios.create({
    baseURL: 'https://localhost:7261/api/v1',
});

const OrdersPage = () => {
    const [orders, setOrders] = useState([]);
    const [loading, setLoading] = useState(true);
    const { customerId: urlCustomerId } = useParams();

    const userData = localStorage.getItem('user');
    const customerId = userData ? JSON.parse(userData).id : urlCustomerId || null;

    useEffect(() => {
        const fetchOrders = async () => {
            if (!customerId) {
                setLoading(false);
                return;
            }

            const token = localStorage.getItem('token');
            if (!token) {
                setLoading(false);
                toast.error("Please log in to view your orders.");
                return;
            }

            try {
                const response = await axiosInstance.get(`/invoices/${customerId}`);
                setOrders(response.data);
            } catch (error) {
                console.error('Failed to fetch orders:', error);
                toast.error('Failed to load order history.');
            } finally {
                setLoading(false);
            }
        };

        fetchOrders();
    }, [customerId]);

    if (loading) {
        return <div className="container my-5 text-center"><h2>Loading your orders...</h2></div>;
    }

    if (!orders.length) {
        return (
            <div className="container my-5 text-center">
                <h2>No orders found.</h2>
            </div>
        );
    }

    return (
        <div className="orders-page container my-5">
            <ToastContainer position="top-center" />
            <h1 className="fw-bold mb-4">Your Order History</h1>
            {orders.map(order => (
                <OrderCard key={order.invoiceId} order={order} />
            ))}
            <Footer />
        </div>
    );
};

export default OrdersPage;