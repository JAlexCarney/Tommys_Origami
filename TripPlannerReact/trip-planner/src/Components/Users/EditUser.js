import React, { useState, useEffect } from 'react';
import ErrorMessage from '../ErrorMessage';

let EditUser = (props) => {
    const [user, setUser] = useState({});
    const [error, setError] = useState("");
    
    useEffect(() => {
        const init = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + props.token
            }
        };
        console.log(props.userID);
        fetch("https://localhost:44365/api/users/" + props.userID, init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("user fetch failed");
                }
                return response.json();
            })
            .then(json => {setUser(json)})
            .catch(console.log);
    }, [props.userID, props.token]);
    
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
        let newUser = {...user};
        delete newUser["confirmPassword"];
        setUser(newUser);
        const init = {
            method: "PUT",
            headers: {
                "Content-Type":"application/json",
                "Accept":"application/json"
            },
            body: JSON.stringify(newUser)
        };
        fetch("https://localhost:44365/api/users", init)
            .then(response => {
                if(response.status !== 200){
                    return Promise.reject("Was not able to edit user.")
                }
            })
            .then(() => {props.changePage("UserProfile");})
            .catch(console.log);
    }
    return(
    <div className="row">
        <div className="col">
            <form onSubmit={onSubmit} className="form-group">
                <div>
                    <label htmlFor="email">New Email</label>
                    <input className="input-wide" type="email" name="email" value={user.email} onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="username">New Username (opt.)</label>
                    <input className="input-wide" type="text" name="username" value={user.username} onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="password">New Password</label>
                    <input className="input-wide" type="password" name="password" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="confirmPassword">Confirm New Password</label>
                    <input className="input-wide" type="password" name="confirmPassword" onChange={handleChange}/>
                </div>
                <ErrorMessage message={error}/>
                <button className="btn btn-primary btn-submit" type="submit">Confirm Update</button><br/>
                <button className="btn btn-secondary btn-submit" type="button" onClick={() => {props.changePage("UserProfile")}}>Cancel</button>
            </form>
        </div>
    </div>
    );
}

export default EditUser;