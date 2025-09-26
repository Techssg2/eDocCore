import React, { useState } from 'react';
import './LoginForm.css';

interface LoginFormProps {
  onLogin: (token: string) => void;
  onShowRegister: () => void;
}

const LoginForm: React.FC<LoginFormProps> = ({ onLogin, onShowRegister }) => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string>('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      const response = await fetch('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
      });
      if (response.ok) {
        const data = await response.json();
        onLogin(data.token);
      } else {
        setError('Đăng nhập thất bại!');
      }
    } catch {
      setError('Lỗi kết nối!');
    }
  };

  return (
    <form className="login-form" onSubmit={handleSubmit}>
      <h2>Đăng nhập</h2>
      {error && <div className="error">{error}</div>}
      <div>
        <label htmlFor="username">Tên đăng nhập:</label>
        <input id="username" value={username} onChange={e => setUsername(e.target.value)} required />
      </div>
      <div>
        <label htmlFor="password">Mật khẩu:</label>
        <input id="password" type="password" value={password} onChange={e => setPassword(e.target.value)} required />
      </div>
      <button type="submit">Đăng nhập</button>
      <button type="button" className="register-btn" onClick={onShowRegister}>Đăng ký</button>
    </form>
  );
};

export default LoginForm;
