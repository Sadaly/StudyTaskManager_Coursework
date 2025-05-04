import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import WeatherForecast from './Components/WeatherForecast.tsx'
import App from './App.tsx'


ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <WeatherForecast />

        <App />
    </React.StrictMode>,
)
