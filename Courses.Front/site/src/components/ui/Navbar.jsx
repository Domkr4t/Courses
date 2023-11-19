import './Navbar.css';

const Navbar = () => {
    return (
      <div>
          <nav >
              <button className='button__menu' onClick={() => window.location.href = "/users"}>Пользователи</button>
              <button className='button__menu' onClick={() => window.location.href = "/courses"}>Курсы</button>
              <button className='button__menu' onClick={() => window.location.href = "/status"}>Статус</button>
          </nav>
      </div>
    );
  };

  export default Navbar
