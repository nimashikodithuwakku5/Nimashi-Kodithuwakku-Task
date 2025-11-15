import axios from 'axios'

// Use Vite env var `VITE_API_URL` in the browser. Falls back to localhost if not provided.
// Prefer VITE_API_URL when provided. Default to port 5206 (what the backend runs on in dev).
const BASE = (typeof import.meta !== 'undefined' && import.meta.env && import.meta.env.VITE_API_URL)
  ? import.meta.env.VITE_API_URL
  : 'http://localhost:5206/api'

const api = axios.create({
  baseURL: BASE,
  timeout: 10000,
})

export default api
