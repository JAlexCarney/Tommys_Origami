import React, { useState } from 'react';
import ErrorMessage from '../ErrorMessage';

let CreateUser = (props) => {
    const [user, setUser] = useState({});
    const [error, setError] = useState("");

    let handleChange = (event) => {
        let newUser = {...user};
        newUser[event.target.name] = event.target.value;
        setUser(newUser);
    }

    let onSubmit = (event) => {
        event.preventDefault();
        if(user.email === undefined || user.email === "")
        {
            setError("Email is required");
            return
        }
        else if(user.password === undefined || user.password === "")
        {
            setError("Password is required");
            return;
        }
        else if(user.confirmPassword !== user.password)
        {
            setError("Passwords do not match");
            return;
        }
        let today = new Date(); //year, month, day
        let dd = String(today.getDate()).padStart(2,"0");
        let mm = String(today.getMonth()+1).padStart(2,"0");
        let yyyy = String(today.getFullYear());
        today = yyyy + "-" + mm + "-" + dd;
        let newUser = {...user};
        console.log(newUser);
        newUser["dateCreated"] = today;
        delete newUser["confirmPassword"];
        setUser(newUser);
        const init = {
            method: "POST",
            headers: {
                "Content-Type":"application/json",
                "Accept":"application/json"
            },
            body: JSON.stringify(newUser)
        };
        fetch("https://localhost:44365/api/users", init)
            .then(response => {
                if(response.status !== 201){
                    return Promise.reject("Was not able to add user.")
                }
                return response.json();
            })
            .then()
            .catch(console.log);
        props.changePage("LoginPage");
    }
    return(
    <div className="row">
        <div className="col">
            <form onSubmit={onSubmit} className="form-group">
                <div>
                    <label htmlFor="email">Email</label>
                    <input className="input-wide" type="email" name="email" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="username">Username (opt.)</label>
                    <input className="input-wide" type="text" name="username" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input className="input-wide" type="password" name="password" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="confirmPassword">Confirm Password</label>
                    <input className="input-wide" type="password" name="confirmPassword" onChange={handleChange}/>
                </div>
                <ErrorMessage message={error}/>
                <button className="btn btn-primary btn-submit" type="submit">Confirm Creation</button><br/>
                <button className="btn btn-secondary btn-submit" type="button" onClick={() => {props.changePage("Login")}}>Cancel</button>
            </form>
        </div>
    </div>
    );
}

export default CreateUser;