import React from 'react';
import ReviewAddForm from './ReviewAddForm.js';
import ReviewEditForm from './ReviewEditForm';
//import ReviewViewForm from './ReviewViewForm';
import ReviewDeleteForm from './ReviewDeleteForm';

let Component = (props) => 
{
    switch(props.form)
    {
        case "Add":
            return (<ReviewAddForm handleAdd={props.action} exitView={props.exitAction}/>);
        case "Edit":
            return (<ReviewEditForm review={props.review} handleEdit={props.action} exitView={props.exitAction}/>);
        case "View":
            //return (<TripViewForm trip={props.trip} exitView={props.exitAction}/>);
        case "Delete":
            return (<ReviewDeleteForm review={props.review} handleDelete={props.action} exitView={props.exitAction}/>);
        default:
            return (<></>);
    }
}
export default Component;
