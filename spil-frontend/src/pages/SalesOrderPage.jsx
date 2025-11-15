import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import { fetchClients } from "../slices/clientsSlice";
import { fetchItems } from "../slices/itemsSlice";
import { fetchOrderById, saveOrder, clearCurrent } from "../slices/ordersSlice";
import OrderRow from "../components/OrderRow";

export default function SalesOrderPage() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();

  const clients = useSelector(s => s.clients?.list ?? []);
  const items = useSelector(s => s.items?.list ?? []);
  const current = useSelector(s => s.orders?.current);

  const [header, setHeader] = useState({ clientId: "", address1: "", address2: "", city: "", orderDate: new Date().toISOString().slice(0,10) });
  const [rows, setRows] = useState([]);

  useEffect(() => { dispatch(fetchClients()); dispatch(fetchItems()); }, [dispatch]);

  useEffect(() => {
    if (id) {
      dispatch(fetchOrderById(id));
    } else {
      dispatch(clearCurrent());
    }
  }, [dispatch, id]);

  useEffect(() => {
    if (current) {
      setHeader({
        clientId: current.clientId ?? current.client?.clientId ?? "",
        address1: current.address1 ?? "",
        address2: current.address2 ?? "",
        city: current.city ?? "",
        orderDate: current.orderDate ? current.orderDate.slice(0,10) : new Date().toISOString().slice(0,10)
      });
      setRows((current.details || []).map(d => ({
        itemId: d.itemId,
        description: d.description,
        note: d.note,
        quantity: d.quantity,
        price: d.price,
        taxRate: d.taxRate || 0,
        excl: d.exclAmount || 0,
        tax: d.taxAmount || 0,
        incl: d.inclAmount || 0
      })));
    }
  }, [current]);

  const onChangeHeader = (field, value) => setHeader(h => ({ ...h, [field]: value }));

  const updateRow = (index, field, value) => {
    setRows(prev => {
      const next = prev.map((r, i) => (i === index ? { ...r } : { ...r }));

      if (field === "itemId") {
        const id = Number(value) || "";
        next[index].itemId = id;
        const itm = items.find(it => Number(it.itemId) === Number(id));
        next[index].description = itm ? itm.description : "";
        next[index].price = itm ? Number(itm.price) : 0;
      } else if (field === "description") {
        const desc = value;
        next[index].description = desc;
        const itm = items.find(it => it.description === desc);
        next[index].itemId = itm ? itm.itemId : "";
        next[index].price = itm ? Number(itm.price) : 0;
      } else if (field === "quantity") {
        next[index].quantity = Number(value);
      } else if (field === "taxRate") {
        next[index].taxRate = Number(value);
      } else {
        next[index][field] = value;
      }

      // compute amounts
      const qty = Number(next[index].quantity || 0);
      const price = Number(next[index].price || 0);
      const taxRate = Number(next[index].taxRate || 0);

      const excl = Math.round(qty * price * 100) / 100;
      const tax = Math.round((excl * taxRate / 100) * 100) / 100;
      const incl = Math.round((excl + tax) * 100) / 100;

      next[index].excl = excl;
      next[index].tax = tax;
      next[index].incl = incl;

      return next;
    });
  };

  const addRow = () => setRows(r => [...r, { itemId: "", description: "", note: "", quantity: 1, price: 0, taxRate: 0, excl: 0, tax: 0, incl: 0 }]);
  const removeRow = (idx) => setRows(r => r.filter((_, i) => i !== idx));

  const totals = rows.reduce((acc, r) => {
    acc.excl += Number(r.excl || 0);
    acc.tax += Number(r.tax || 0);
    acc.incl += Number(r.incl || 0);
    return acc;
  }, { excl: 0, tax: 0, incl: 0 });

  const onSave = async () => {
    const payload = {
      clientId: header.clientId,
      orderDate: header.orderDate,
      address1: header.address1,
      address2: header.address2,
      city: header.city,
      details: rows.map(r => ({ itemId: r.itemId, description: r.description, note: r.note, quantity: r.quantity, price: r.price, taxRate: r.taxRate, exclAmount: r.excl, taxAmount: r.tax, inclAmount: r.incl }))
    };
    try {
      const res = await dispatch(saveOrder(payload)).unwrap();
      // navigate back to list or to created id
      navigate("/");
    } catch (err) {
      console.error(err);
      alert("Save failed. See console.");
    }
  };

  // when client selected, fill address
  useEffect(() => {
    const cid = header.clientId;
    if (cid) {
      const c = clients.find(x => String(x.clientId) === String(cid));
      if (c) setHeader(h => ({ ...h, address1: c.address1 ?? "", address2: c.address2 ?? "", city: c.city ?? "" }));
    }
  }, [header.clientId, clients]);

  return (
    <div className="min-h-screen p-6 app-container">
      {/* Header / Title Bar */}
      <div className="flex items-center justify-between mb-6 page-header">
        {/* Left: window controls (visual placeholders) */}
        <div className="flex items-center gap-2">
          <div className="w-3 h-3 rounded-full bg-gray-300" title="minimize" />
          <div className="w-3 h-3 rounded-full bg-gray-300" title="maximize" />
          <div className="w-3 h-3 rounded-full bg-red-400" title="close" />
        </div>

        {/* Center: Title */}
        <div className="text-center">
          <h1 className="brand-heading title-large">Sales Order</h1>
        </div>

        {/* Right: Save Order button */}
        <div className="flex items-center gap-2">
          <button onClick={onSave} className="btn-primary flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor" aria-hidden>
              <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-7.071 7.071a1 1 0 01-1.414 0L3.293 9.848a1 1 0 011.414-1.414L8 11.727l6.364-6.364a1 1 0 011.343-.07z" clipRule="evenodd" />
            </svg>
            <span>Save Order</span>
          </button>
        </div>
      </div>

      {/* Customer and Invoice Details */}
      <div className="grid grid-cols-3 gap-6 mb-6">
        {/* Customer Details (left - span 2) */}
        <div className="col-span-2 card-elevated p-4 group-bracket">
          <div className="flex">
            <div className="pl-4 flex-1">
              <div className="mb-3">
                <label className="block text-sm font-medium text-gray-700">Customer Name</label>
                <select className="input mt-1" value={header.clientId ?? ""} onChange={(e) => onChangeHeader('clientId', e.target.value)}>
                  <option value="">-- select --</option>
                  {clients.map(c => <option key={c.clientId} value={c.clientId}>{c.name}</option>)}
                </select>
              </div>

              <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
                <div>
                  <label className="block text-sm text-gray-600">Address 1</label>
                  <input className="input" value={header.address1 ?? ""} onChange={(e) => onChangeHeader('address1', e.target.value)} />
                </div>
                <div>
                  <label className="block text-sm text-gray-600">Address 2</label>
                  <input className="input" value={header.address2 ?? ""} onChange={(e) => onChangeHeader('address2', e.target.value)} />
                </div>
                <div>
                  <label className="block text-sm text-gray-600">Address 3</label>
                  <input className="input" value={header.address3 ?? ""} onChange={(e) => onChangeHeader('address3', e.target.value)} />
                </div>
              </div>

              <div className="grid grid-cols-1 md:grid-cols-3 gap-3 mt-3">
                <div>
                  <label className="block text-sm text-gray-600">City</label>
                  <input className="input" value={header.city ?? ""} onChange={(e) => onChangeHeader('city', e.target.value)} />
                </div>
                <div>
                  <label className="block text-sm text-gray-600">State</label>
                  <input className="input" value={header.state ?? ""} onChange={(e) => onChangeHeader('state', e.target.value)} />
                </div>
                <div>
                  <label className="block text-sm text-gray-600">Post Code</label>
                  <input className="input" value={header.postalCode ?? ""} onChange={(e) => onChangeHeader('postalCode', e.target.value)} />
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Invoice Details (right) */}
        <div className="card-elevated p-4">
          <div>
            <label className="block text-sm font-medium text-gray-700">Invoice No.</label>
            <input className="input mt-1" value={header.invoiceNo ?? ""} onChange={(e) => onChangeHeader('invoiceNo', e.target.value)} />
          </div>

          <div className="grid grid-cols-1 gap-3 mt-3">
            <div>
              <label className="block text-sm text-gray-600">Invoice Date</label>
              <input type="date" className="input" value={header.orderDate} onChange={(e) => onChangeHeader('orderDate', e.target.value)} />
            </div>
            <div>
              <label className="block text-sm text-gray-600">Reference No</label>
              <input className="input" value={header.referenceNo ?? ""} onChange={(e) => onChangeHeader('referenceNo', e.target.value)} />
            </div>
          </div>
        </div>
      </div>

      {/* Item Details Table */}
      <div className="card-elevated p-4 mb-6">
        <div className="overflow-x-auto">
          <table className="min-w-full text-sm table-auto">
            <thead className="bg-gray-100">
              <tr>
                <th className="p-2 text-left">Item Code</th>
                <th className="p-2 text-left">Description</th>
                <th className="p-2 text-left">Note</th>
                <th className="p-2 text-right">Quantity</th>
                <th className="p-2 text-right">Price</th>
                <th className="p-2 text-right">Tax %</th>
                <th className="p-2 text-right">Excl Amount</th>
                <th className="p-2 text-right">Tax Amount</th>
                <th className="p-2 text-right">Incl Amount</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((r, idx) => (
                <React.Fragment key={idx}>
                  <OrderRow row={r} items={items} onChange={(field, value) => updateRow(idx, field, value)} />
                  <tr>
                    <td colSpan={9} className="p-2 text-right">
                      <button className="text-sm text-red-600" onClick={() => removeRow(idx)}>Remove</button>
                    </td>
                  </tr>
                </React.Fragment>
              ))}
            </tbody>
          </table>
        </div>

        <div className="flex justify-between items-center mt-4">
          <button onClick={addRow} className="btn-add">Add Line</button>

          {/* Total Summary (bottom right) */}
          <div className="text-right bg-gradient-to-r from-white/50 to-white/30 p-3 rounded-lg">
            <div className="text-sm muted">Total Excl: <span className="font-medium">{totals.excl.toFixed(2)}</span></div>
            <div className="text-sm muted">Total Tax: <span className="font-medium">{totals.tax.toFixed(2)}</span></div>
            <div className="text-xl font-bold">Total Incl: <span className="ml-2">{totals.incl.toFixed(2)}</span></div>
          </div>
        </div>
      </div>
    </div>
  );
}
