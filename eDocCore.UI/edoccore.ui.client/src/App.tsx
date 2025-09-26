import React, { useEffect, useState } from 'react';
import LoginForm from './components/LoginForm/LoginForm';
import RoleList from './components/RoleList/RoleList';
import Register from './pages/Register/Register';
import './App.css';

interface Forecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const App: React.FC = () => {
  const [forecasts, setForecasts] = useState<Forecast[] | undefined>();
  const [token, setToken] = useState<string | null>(null);
  const [showRegister, setShowRegister] = useState<boolean>(false);

  useEffect(() => {
    populateWeatherData();
  }, []);

  const contents = forecasts === undefined
    ? <p><em>?ang t?i... Vui l�ng l�m m?i trang sau khi backend ASP.NET ?� kh?i ??ng. Xem <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> ?? bi?t th�m chi ti?t.</em></p>
    : <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Ng�y</th>
            <th>Nhi?t ?? (C)</th>
            <th>Nhi?t ?? (F)</th>
            <th>T�m t?t</th>
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
    if (showRegister) {
      return <Register />;
    }
    return <LoginForm onLogin={setToken} onShowRegister={() => setShowRegister(true)} />;
  }

  return (
    <div>
      <h1 id="tableLabel">D? b�o th?i ti?t</h1>
      <p>Th�nh ph?n n�y minh h?a vi?c l?y d? li?u t? m�y ch?.</p>
      {contents}
      <RoleList />
    </div>
  );

  async function populateWeatherData() {
    const response = await fetch('weatherforecast');
    if (response.ok) {
      const data = await response.json();
      setForecasts(data);
    }
  }
};

export default App;
