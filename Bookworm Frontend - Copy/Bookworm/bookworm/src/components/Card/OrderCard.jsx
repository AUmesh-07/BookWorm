import React from 'react';
import { FaFileInvoiceDollar, FaRegCalendarAlt } from 'react-icons/fa';

const OrderCard = ({ order }) => (
    <div className="card shadow-sm mb-4">
        <div className="card-header bg-white d-flex justify-content-between align-items-center">
            <h5 className="fw-bold mb-0">
                <FaFileInvoiceDollar className="me-2 text-pink" />
                Invoice ID: {order.invoiceId}
            </h5>
            <div className="text-muted d-flex align-items-center">
                <FaRegCalendarAlt className="me-1" />
                Date: {new Date(order.date).toLocaleDateString()}
            </div>
        </div>
        <div className="card-body">
            <h6 className="fw-bold mb-3">Order Details</h6>
            <ul className="list-group list-group-flush">
                {order.items.map((item, index) => (
                    <li key={index} className="list-group-item d-flex justify-content-between align-items-center">
                        <div>
                            <span className="fw-medium">{item.productName}</span>
                            <span className="badge bg-light text-dark ms-2">{item.tranType}</span>
                        </div>
                        <span className="fw-bold">₹{item.sellPrice.toFixed(2)}</span>
                    </li>
                ))}
            </ul>
        </div>
        <div className="card-footer bg-light d-flex justify-content-end align-items-center">
            <span className="fw-bold fs-5 text-success">Total Amount: ₹{order.amount.toFixed(2)}</span>
        </div>
    </div>
);

export default OrderCard;