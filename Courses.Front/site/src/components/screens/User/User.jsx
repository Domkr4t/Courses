import React, { useState, useEffect, Fragment } from 'react';
import "/src/assets/styles/User.css";

const User = () => {
    const [users, setUsers] = useState([]);
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [role, setRole] = useState('');
    const [filterName, setFilterName] = useState('');
    const [filterRole, setFilterRole] = useState('Студент');
    const [filterCourse, setFilterCourse] = useState('');

    const fetchData = async () => {
        try {
          const response = await fetch(`https://localhost:7114/User/UserHandler?name=${filterName}&role=${filterRole}&course=${filterCourse}`);
          if (response.ok) {
            const responseData = await response.json();
            const data = responseData.data;
            const users = Object.values(data);
            setUsers(users);
          } else {
            console.error('Ошибка при получении данных');
          }
        } catch (error) {
          console.error('Ошибка при получении данных', error);
        }
      };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const userData = {
          name,
          password,
          email,
          role,
        };

        try {
            const response = await fetch('https://localhost:7114/User/Create', {
                method: 'POST',
                headers: {
                  'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    name: name,
                    password: password,
                    email: email,
                    role: role,
                }),
              });


            if (response.ok) {
                setName('');
                setPassword('');
                setEmail('');
                setRole('');
                fetchData();
            } else {
                console.error('Ошибка при отправке данных');
            }
        } catch (error) {
          console.error(error);
        }
      };

    const handleDelete = async (index) => {
    try {
        const response = await fetch('https://localhost:7114/User/Delete', {
                method: 'POST',
                headers: {
                  'Content-Type': 'application/json',
                },
                body: index,
              });
        if (response.ok) {
            const updatedUsers = [...users];
            updatedUsers.splice(index, 1);
            setUsers(updatedUsers);
            fetchData();
        } else {
            console.error('Ошибка при отправке данных');
        }
    } catch (error) {
        console.error(error);
    }
    };

    const handleNameChange = (e) => {
        const value = e.target.value;
        setFilterName(value);
      };

    const handleRoleChange = (e) => {
        const value = e.target.value;
        setFilterRole(value);
    };

    const handleCourseChange = (e) => {
        const value = e.target.value;
        setFilterCourse(value);
      };

    useEffect(() => {
        fetchData();
    }, [filterName, filterRole, filterCourse]);

return (
        <div>
        <form onSubmit={handleSubmit}>
            <div className ="user_beatifull_word" for="login">Общая информация</div>
            <input className= 'user_text-field__input' name="name" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder='ФИО' />
            <input className= 'user_text-field__input' name="password" type="text" value={password} onChange={(e) => setPassword(e.target.value)} placeholder='Пароль'/>
            <input className= 'user_text-field__input' name="email" type="text" value={email} onChange={(e) => setEmail(e.target.value)} placeholder='Email'/>
            <input className= 'user_text-field__input' name="role" type="text" value={role} onChange={(e) => setRole(e.target.value)} placeholder='Роль'/>
            <button className = 'user_button' type="submit">Сохранить</button>
        </form>
        <div className ="user_beatifull_word" for="login">Фильтрация</div>
        <input className='user_textFilter-field__input' name="filterName" type="text" value={filterName} onChange={handleNameChange} placeholder='Фильтр по ФИО' />
        <input className='user_textFilter-field__input' name="filterRole" type="text" value={filterRole} onChange={handleRoleChange} placeholder='Фильтр по роли' />
        <input className='user_textFilter-field__input' name="filterCourse" type="text" value={filterCourse} onChange={handleCourseChange} placeholder='Фильтр по курсам' />
          <table className='user_table'>
            <thead>
              <tr>
                <th>ФИО</th>
                <th>Пароль</th>
                <th>Email</th>
                <th>Роль</th>
                <th>Курсы</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
                {users && users.map((user, index) => (
                <tr key={index}>
                    <td>{user.name}</td>
                    <td>{user.password}</td>
                    <td>{user.email}</td>
                    <td>{user.role}</td>
                    <td>{user.courses}</td>
                    <td>
                        <button onClick={() => handleDelete(user.id)}>Удалить</button>
                    </td>
                </tr>
            ))}
            </tbody>
          </table>
        </div>
      );
};

export default User
