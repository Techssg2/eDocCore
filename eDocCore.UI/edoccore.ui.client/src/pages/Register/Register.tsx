import React, { useState } from 'react';
import './Register.css';

const Register: React.FC = () => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [confirmPassword, setConfirmPassword] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [success, setSuccess] = useState<string>('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setSuccess('');
    if (password !== confirmPassword) {
      setError('Mật khẩu xác nhận không khớp!');
      return;
    }
    try {
      const response = await fetch('/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
      });
      if (response.ok) {
        setSuccess('Đăng ký thành công!');
        // Tự động đăng nhập sau khi đăng ký thành công
        const loginResponse = await fetch('/user/login', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ username, password })
        });
        if (loginResponse.ok) {
          setSuccess('Đăng ký và đăng nhập thành công!');
          // Có thể xử lý lưu token ở đây nếu cần
        } else {
          setError('Đăng ký thành công nhưng đăng nhập thất bại!');
        }
        setUsername('');
        setPassword('');
        setConfirmPassword('');
      } else {
        setError('Đăng ký thất bại!');
      }
    } catch {
      setError('Lỗi kết nối!');
    }
  };

  return (
    <form className="register-form" onSubmit={handleSubmit}>
      <h2>Đăng ký tài khoản</h2>
      {error && <div className="error">{error}</div>}
      {success && <div className="success">{success}</div>}
      <div>
        <label htmlFor="username">Tên đăng nhập:</label>
        <input id="username" value={username} onChange={e => setUsername(e.target.value)} required />
      </div>
      <div>
        <label htmlFor="password">Mật khẩu:</label>
        <input id="password" type="password" value={password} onChange={e => setPassword(e.target.value)} required />
      </div>
      <div>
        <label htmlFor="confirmPassword">Xác nhận mật khẩu:</label>
        <input id="confirmPassword" type="password" value={confirmPassword} onChange={e => setConfirmPassword(e.target.value)} required />
      </div>
      <button type="submit">Đăng ký</button>
    </form>
  );    
};

export default Register;
