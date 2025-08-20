import React from "react";

export default function ProductTable({ products, onEdit, onDelete }) {
  return (
    <div className="table-responsive">
      <table className="table table-striped table-bordered align-middle">
        <thead>
          <tr>
            <th>ID</th>
            <th>Cover</th>
            <th>Name</th>
            <th>Author</th>
            <th>ISBN</th>
            <th>Genre</th>
            <th>Language</th>
            <th>Type</th>
            <th>Rent/Day</th>
            <th>Rentable</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map((p) => (
            <tr key={p.id}>
              <td>{p.id}</td>
              <td>
                <img
                  src={p.imageSource} // ✅ Assuming backend sends `imgSrc` or adapt to match key
                  alt={p.englishName}
                  style={{ width: "60px", height: "auto", objectFit: "cover" }}
                />
              </td>
              <td>{p.englishName}</td>
              <td>{p.author}</td>
              <td>{p.isbn}</td>
              <td>{p.genreName}</td>
              <td>{p.languageName}</td>
              <td>{p.productTypeName}</td>
              <td>₹{p.rentPerDay}</td>
              <td>{p.rentable ? "Yes" : "No"}</td>
              <td>
                <button
                  className="btn btn-sm btn-warning me-2"
                  onClick={() => onEdit(p)}
                >
                  Edit
                </button>
                <button
                  className="btn btn-sm btn-danger"
                  onClick={() => onDelete(p.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
