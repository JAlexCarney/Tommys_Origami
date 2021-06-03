import React from 'react';

let Login = () => {
    return (
        <div>
            Hello World!
            <form className="form-group" id="login">
                <div>
                    <label htmlFor="email" >Email</label>
                    <input type="text" />
                </div>
            </form>
        </div>
    )
}

export default Login;