import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [error, setError] = useState("");
  const [customers, setCustomers] = useState<ICustomer[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch("/api/customer");
      if(!response.ok) {
        setError(response.statusText);
        return;
      }
      const data = await response.json();
      setCustomers(data as ICustomer[]);
    }

    fetchData();
  }, []);

  return (
    <>
      {error !== "" ? <div className='error'>{error}</div>
      : <ul className='list'>
        {
          customers.map((customer, idx) => (
            <li key={idx}>
              <div className='customer'>
                <div className='c-name'>{customer.name}</div>
                <div>{customer.street}</div>
                <div>{customer.postCode} {customer.city}</div>
              </div>
            </li>
          ))
        }
        </ul>}
    </>
  )
}

export default App

interface ICustomer {
  id: string,
  name: string,
  street: string,
  postCode: string,
  city: string
}