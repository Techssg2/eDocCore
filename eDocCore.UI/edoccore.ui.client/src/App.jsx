import { useEffect, useState } from 'react';
import LoginForm from './LoginForm';
import './App.css';

function App() {
    const [forecasts, setForecasts] = useState();
    const [token, setToken] = useState(null);

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>?ang t?i... Vui lòng làm m?i trang sau khi backend ASP.NET ?ã kh?i ??ng. Xem <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> ?? bi?t thêm chi ti?t.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Ngày</th>
                    <th>Nhi?t ?? (C)</th>
                    <th>Nhi?t ?? (F)</th>
                    <th>Tóm t?t</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    if (!token) {
        return <LoginForm onLogin={setToken} />;
    }

    return (
        <div>
            <h1 id="tableLabel">D? báo th?i ti?t</h1>
            <p>Thành ph?n này minh h?a vi?c l?y d? li?u t? máy ch?.</p>
            {contents}
        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        if (response.ok) {
            const data = await response.json();
            setForecasts(data);
        }
    }
}

export default App;