import React, { useEffect, useState } from 'react';
import "/src/assets/styles/Status.css";

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
    <div >
        <table className='status_table'>
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
