import { BrowserRouter, Route, Routes } from "react-router-dom";
import Navbar from "./Navbar";
import User from "../screens/User/User";
import Course from "../screens/Course/Course";
import Status from "../screens/Status/Status";


const Router = () => {
    return <BrowserRouter>
        <Navbar />
        <Routes>
            <Route element={<User />} path='/users' />
            {/* <Route element={<UserPage />} path='/userPage/:id' /> */}

            <Route element={<Course />} path='/courses' />
            {/* <Route element={<CoursePage />} path='/coursePage/:id' /> */}

            <Route element={<Status />} path='/status' />

            <Route path='*' element={<div>Not found</div>}/>
        </Routes>
    </BrowserRouter>
}

export default Router
