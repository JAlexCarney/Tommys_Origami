import React from 'react';
import TripAddForm from './TripAddForm.js';
import TripEditForm from './TripEditForm';
import TripViewForm from './TripViewForm';
import TripDeleteForm from './TripDeleteForm';

let Component = (props) => 
{
    switch(props.form)
    {
        case "Add":
            return (<TripAddForm handleAdd={props.action} exitView={props.exitAction}/>);
        case "Edit":
            return (<TripEditForm trip={props.trip} handleEdit={props.action} exitView={props.exitAction}/>);
        case "View":
            return (<TripViewForm trip={props.trip} exitView={props.exitAction}/>);
        case "Delete":
            return (<TripDeleteForm trip={props.trip} handleDelete={props.action} exitView={props.exitAction}/>);
        default:
            return (<div></div>);
    }
}
export default Component;