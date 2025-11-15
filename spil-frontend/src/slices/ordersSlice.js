import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import api from "../services/api";

export const fetchOrders = createAsyncThunk("orders/fetch", async () => {
  const res = await api.get("/salesorders");
  return res.data;
});

export const fetchOrderById = createAsyncThunk("orders/fetchById", async (id) => {
  const res = await api.get(`/salesorders/${id}`);
  return res.data;
});

export const saveOrder = createAsyncThunk("orders/save", async (payload) => {
  // backend expects a CreateOrderRequest shape; flatten if needed by backend
  const body = {
    clientId: payload.clientId ?? payload.header?.clientId ?? payload.header?.clientId,
    orderDate: payload.orderDate ?? payload.header?.orderDate ?? payload.header?.orderDate,
    address1: payload.address1 ?? payload.header?.address1 ?? payload.header?.address1,
    address2: payload.address2 ?? payload.header?.address2 ?? payload.header?.address2,
    city: payload.city ?? payload.header?.city ?? payload.header?.city,
    details: payload.details ?? payload.details
  };
  const res = await api.post("/salesorders", body);
  return res.data;
});

const ordersSlice = createSlice({
  name: "orders",
  initialState: { list: [], current: null, status: "idle" },
  reducers: {
    clearCurrent(state) { state.current = null; }
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchOrders.fulfilled, (s, a) => { s.list = a.payload; })
      .addCase(fetchOrderById.fulfilled, (s, a) => { s.current = a.payload; })
      .addCase(saveOrder.fulfilled, (s, a) => { /* you may push or refetch */ });
  }
});

export const { clearCurrent } = ordersSlice.actions;
export default ordersSlice.reducer;
