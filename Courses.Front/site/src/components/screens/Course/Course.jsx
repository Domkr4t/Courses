import React, { useState, useEffect } from 'react';
import './Course.css';

const Course = () => {
    const [users, setUsers] = useState([]);
    const [courses, setCourses] = useState([]);
    const [selectedStudents, setSelectedStudents] = useState([]);
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [teacher, setTeacher] = useState('');
    const [filterTitle, setFilterTitle] = useState('');
    const [filterTeacher, setFilterTeacher] = useState('');

    const fetchUsers = async () => {
        try {
        const response = await fetch('https://localhost:7114/User/UserHandler', {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
            },
          });
        if (response.ok) {
            const responseData = await response.json();
            const data = responseData.data;
            const usersArray = Object.values(data);
            setUsers(usersArray);
        } else {
            console.error('Ошибка при получении данных');
        }
        } catch (error) {
        console.error('Ошибка при получении данных', error);
        }
    };

    const fetchData = async () => {
        try {
            const response = await fetch(`https://localhost:7165/Course/CourseHandler?title=${filterTitle}&teacher=${filterTeacher}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });
        if (response.ok) {
            const responseData = await response.json();
            const data = responseData.data;
            console.log(data);
            const coursesArray = Object.values(data);
            console.log(coursesArray);
            setCourses(coursesArray);
        } else {
            console.error('Ошибка при получении данных');
        }
        } catch (error) {
        console.error('Ошибка при получении данных', error);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('https://localhost:7165/Course/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    title: title,
                    description: description,
                    teacher: teacher,
                    students: selectedStudents
                }),
            });
            if (response.ok) {
                setTitle('');
                setDescription('');
                setTeacher('');
                fetchData();
            } else {
                console.error('Ошибка при отправке данных');
            }
        } catch (error) {
            console.error('Ошибка при отправке данных', error);
        }
    };

    const handleDelete = async (id) => {
        try {
            const response = await fetch('https://localhost:7165/Course/Delete', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: id,
                });
            if (response.ok) {
                const updatedCourses = [...courses];
                const index = updatedCourses.findIndex(course => course.id === id);
                updatedCourses.splice(index, 1);
                setCourses(updatedCourses);
                fetchData();
            } else {
                console.error('Ошибка при отправке данных');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleCheckboxChange = (event) => {
        const studentName = event.target.value;
        if (event.target.checked) {
          setSelectedStudents([...selectedStudents, studentName]);

        } else {
          setSelectedStudents(selectedStudents.filter(student => student !== studentName));
        }
      };

    const handleTitleChange = (e) => {
        const value = e.target.value;
        setFilterTitle(value);
    };

    const handleTeacherChange = (e) => {
        const value = e.target.value;
        setFilterTeacher(value);
    };

    useEffect(() => {
        fetchData();
        fetchUsers();
    }, [selectedStudents, filterTitle, filterTeacher]);

return (
        <div>
            <form onSubmit={handleSubmit}>
                <div className ="beatifull_word" for="login">Общая информация</div>
                <input className = "text-field__input" name="title" type="text" value={title} onChange={(e) => setTitle(e.target.value)} placeholder="Название" />
                <input className = "text-field__input" name="teacher" type="text" value={teacher} onChange={(e) => setTeacher(e.target.value)} placeholder="Преподаватель" />
                <input className = "text-field__input__big" name="description" type="text" value={description} onChange={(e) => setDescription(e.target.value)} placeholder="Описание" />
                <div className ="beatifull_word" for="login">Участники</div>
                {users.map((user) => (
                <div key={user.id}>
                    <input cl
                        name="user"
                        type="checkbox"
                        value={user.name}
                        checked={selectedStudents.includes(user.name)}
                        onChange={handleCheckboxChange}
                    />
                    <label>{user.name}</label>
                </div>
                ))}
                <button className='button' type="submit">Сохранить</button>
            </form>
            <div className ="beatifull_word" for="login">Фильтрация</div>
            <input className = "textFilter-field__input" name="filterTitle" type="text" value={filterTitle} onChange={handleTitleChange} placeholder='Фильтр по названию' />
            <input className = "textFilter-field__input" name="filterTeacher" type="text" value={filterTeacher} onChange={handleTeacherChange} placeholder='Фильтр по преподавателю' />
            <table className='table'>
                <thead>
                <tr>
                    <th>Название</th>
                    <th>Описание</th>
                    <th>Учитель</th>
                    <th>Дата создания</th>
                    <th>Участники</th>
                    <th></th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                {courses && courses
                .map((course, index) => (
                    <tr key={index}>
                        <td>{course.title}</td>
                        <td>{course.description}</td>
                        <td>{course.teacher}</td>
                        <td>{course.created}</td>
                        <td>{course.students}</td>
                        <td>
                            <button onClick={() => window.location.href = `/coursePage/${course.id}`}>Просмотреть</button>
                        </td>
                        <td>
                            <button onClick={() => handleDelete(course.id)}>Удалить</button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>

    );
}

export default Course
