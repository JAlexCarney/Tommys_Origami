import React, {useState, useEffect} from 'react';
import DestinationTrip from '../DestinationTrip/DestinationTrip';

let Component = (props) => 
{
    const [state, setState] = useState({});
    const [editedDestinations, setEditedDestinations] = useState([]);

    useEffect(() => {
        setState(props.trip);
    }, [props.trip]);

    const handleChange = (event) => {
        let newState = { ...state };

        newState[event.target.name] = event.target.value;

        setState(newState);
    };

    const handleCheck = (event) => {
        let newState = { ...state };
        if(event.target.value === "on")
        {
            newState[event.target.name] = true;
        }
        else
        {
            newState[event.target.name] = false;
        }
        setState(newState);
    }

    const editDestinations = (destinations) => {
        setEditedDestinations(destinations);
    }
    return (
        <div className="form">
            <h3 className="form-header">Editing Trip{" " + props.trip.tripID}</h3>
            <div className="form-field">
                <label className="label-small" htmlFor="startDate">Start Date:&ensp;</label>
                <input className="input-small" type="date" defaultValue={props.trip.startDate.slice(0, 10)} name="startDate" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label className="label-small" htmlFor="projectedEndDate">End Date:&ensp;</label>
                <input className="input-small" type="date" defaultValue={props.trip.projectedEndDate.slice(0, 10)} name="projectedEndDate" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label className="label-small">Booked:&ensp;</label>
                <input type="checkbox" defaultChecked={props.trip.isBooked} name="isBooked" onChange={handleCheck}></input>
            </div>
            <DestinationTrip token={props.token} isAdd={false} tripID={props.trip.tripID} editDestinations={editDestinations}/>
        <form key={props.trip.tripID} onSubmit={(event) => {event.preventDefault(); props.handleEdit(state, editedDestinations);}}>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Edit</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;