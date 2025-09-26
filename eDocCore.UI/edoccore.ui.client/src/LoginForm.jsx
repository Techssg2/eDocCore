import { useState } from 'react';

function LoginForm({ onLogin }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e) => {
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
        onLogin(data.token); // truyền token lên App
      } else {
        setError('Đăng nhập thất bại!');
      }
    } catch {
      setError('Lỗi kết nối!');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Đăng nhập1</h2>
      {error && <div style={{color:'red'}}>{error}</div>}
      <div>
        <label>Tên đăng nhập:</label>
        <input value={username} onChange={e => setUsername(e.target.value)} required />
      </div>
      <div>
        <label>Mật khẩu:</label>
        <input type="password" value={password} onChange={e => setPassword(e.target.value)} required />
      </div>
      <button type="submit">Đăng nhập</button>
    </form>
  );
}

export default LoginForm;
