import React from 'react';

let ErrorMessage = (props) => {
    if(props.message !== "")
    {
        return <div className="my-error-container"><div className="my-error" role="alert">{props.message}</div></div>
    }
    return <div></div>;
}

export default ErrorMessage;