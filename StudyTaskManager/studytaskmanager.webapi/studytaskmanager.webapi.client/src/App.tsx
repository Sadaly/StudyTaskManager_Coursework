import { useEffect, useState } from "react";
import UsersList from "./Components/UsersList";
import { UserResponse } from "./TypesFromTheServer/UserResponse";
import axios from "axios"; 
function App() {

    const [users, setUsers] = useState<UserResponse[]>([])

    useEffect(() => {
        populateWeatherData()
    }, [])

    async function fetchUsers() {
        try {
            const response = await axios.get<UserResponse[]>('https://localhost:7241/api/Users/All')
            setUsers(response.data)
        } catch (e) {
            alert(e)
        }
    }

    async function populateWeatherData() {
        const response = await fetch('api/Users/All');
        const data = await response.json();
        setUsers(data);
    }

    return (
        <div>
            <p>---</p>
            <UsersList users={users} />
            <p>---</p>
        </div>
        //<BrowserRouter>
        //    <Routes>
        //        <Route path="/login" element={<Login />} />
        //        <Route path="/register" element={<Register />} />
        //        <Route path="/" element={<Home />} />
        //    </Routes>
        //</BrowserRouter>
    );

}
export default App;