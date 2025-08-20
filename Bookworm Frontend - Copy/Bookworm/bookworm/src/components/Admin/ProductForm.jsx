import React, { useEffect, useState } from 'react';

export default function ProductForm({ genres, languages, types, onSave, selected, clearForm }) {
  const [form, setForm] = useState({
    englishName: '',
    isbn: '',
    author: '',
    longDescription: '',
    shortDescription: '',
    imageSource: '',
    minRentDays: '',
    rentPerDay: '',
    basePrice: '',
    offerPrice: '',
    specialCost: '',
    genreId: '',
    languageId: '',
    productTypeId: '',
    rentable: false
  });

  useEffect(() => {
    if (selected) {
      setForm(selected);
    } else {
      setForm({
        englishName: '',
        isbn: '',
        author: '',
        longDescription: '',
        shortDescription: '',
        imageSource: '',
        minRentDays: '',
        rentPerDay: '',
        basePrice: '',
        offerPrice: '',
        specialCost: '',
        genreId: '',
        languageId: '',
        productTypeId: '',
        rentable: false
      });
    }
  }, [selected]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setForm({ ...form, [name]: type === 'checkbox' ? checked : value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const payload = {
      ...form,
      genreId: Number(form.genreId),
      languageId: Number(form.languageId),
      productTypeId: Number(form.productTypeId)
    };
    onSave(payload);
    setForm({
      englishName: '',
      isbn: '',
      author: '',
      longDescription: '',
      shortDescription: '',
      imageSource: '',
      minRentDays: '',
      rentPerDay: '',
      basePrice: '',
      offerPrice: '',
      specialCost: '',
      genreId: '',
      languageId: '',
      productTypeId: '',
      rentable: false
    });
  };

  return (
    <form onSubmit={handleSubmit} className="p-3 border rounded mb-4 bg-light">
      <div className="row g-2">
        <div className="col-md-4">
          <input type="text" className="form-control" name="englishName" value={form.englishName} onChange={handleChange} placeholder="English Name" required />
        </div>
        <div className="col-md-4">
          <input type="text" className="form-control" name="isbn" value={form.isbn} onChange={handleChange} placeholder="ISBN" required />
        </div>
        <div className="col-md-4">
          <input type="text" className="form-control" name="author" value={form.author} onChange={handleChange} placeholder="Author" required />
        </div>
        <div className="col-md-6">
          <input type="text" className="form-control" name="shortDescription" value={form.shortDescription} onChange={handleChange} placeholder="Short Description" />
        </div>
        <textarea className="form-control" name="longDescription" value={form.longDescription} onChange={handleChange} placeholder="Long Description" rows="3" />
        <div className="col-md-6">
          <input type="text" className="form-control" name="imageSource" value={form.imageSource} onChange={handleChange} placeholder="Image URL" />
        </div>
        <div className="col-md-2">
          <input type="number" className="form-control" name="minRentDays" value={form.minRentDays} onChange={handleChange} placeholder="Min Rent Days" />
        </div>
        <div className="col-md-2">
          <input type="number" className="form-control" name="rentPerDay" value={form.rentPerDay} onChange={handleChange} placeholder="Rent/Day" />
        </div>
        <div className="col-md-2">
          <input type="number" className="form-control" name="basePrice" value={form.basePrice} onChange={handleChange} placeholder="Base Price" />
        </div>
        <div className="col-md-2">
          <input type="number" className="form-control" name="offerPrice" value={form.offerPrice} onChange={handleChange} placeholder="Offer Price" />
        </div>
        <div className="col-md-2">
          <input type="number" className="form-control" name="specialCost" value={form.specialCost} onChange={handleChange} placeholder="Special Cost" />
        </div>
        <div className="col-md-2">
          <select className="form-select" name="genreId" value={form.genreId} onChange={handleChange} required>
            <option value="">Select Genre</option>
            {genres.map(g => <option key={g.id} value={g.id}>{g.name}</option>)}
          </select>
        </div>
        <div className="col-md-2">
          <select className="form-select" name="languageId" value={form.languageId} onChange={handleChange} required>
            <option value="">Select Language</option>
            {languages.map(l => <option key={l.id} value={l.id}>{l.name}</option>)}
          </select>
        </div>
        <div className="col-md-2">
          <select className="form-select" name="productTypeId" value={form.productTypeId} onChange={handleChange} required>
            <option value="">Select Type</option>
            {types.map(t => <option key={t.id} value={t.id}>{t.name}</option>)}
          </select>
        </div>
        <div className="col-md-2 d-flex align-items-center">
          <label className="form-check-label me-2">Rentable</label>
          <input type="checkbox" className="form-check-input" name="rentable" checked={form.rentable} onChange={handleChange} />
        </div>
        <div className="col-md-4 text-end">
          <button type="submit" className="btn btn-primary me-2">Save</button>
          <button type="button" className="btn btn-secondary" onClick={clearForm}>Clear</button>
        </div>
      </div>
    </form>
  );
}
