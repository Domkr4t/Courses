import React, { useEffect, useState } from 'react';
import './Status.css';

const StatusComponent = () => {
  const [statuses, setStatuses] = useState([]);

  useEffect(() => {
    const fetchStatus = async () => {
      try {
        const response = await fetch('http://localhost:8081/status');
        const data = await response.json();
        setStatuses(data);
      } catch (error) {
        console.error('Error fetching status:', error);
      }
    };

    fetchStatus();
  }, []);

  return (
    // <div>
    //   <h1>Status of Microservices</h1>
    //   <ul>
    //     {statuses.map((status, index) => (
    //       <li key={index}>
    //         <strong>{status.service_name}:</strong> {status.status}
    //         {status.message && <span> - {status.message}</span>}
    //       </li>
    //     ))}
    //   </ul>
    // </div>

    <div >
        <table className='table'>
            <thead>
                <tr>
                <th>Микросервис</th>
                <th>Состояние</th>
                </tr>
            </thead>
        <tbody>
        {statuses && statuses.map((status, index) => (
            <tr>
                <td>{status.service_name}</td>
                <td>{status.status}</td>
            </tr>
        ))}
        </tbody>
        </table>
    </div>
  );
};

export default StatusComponent;
