import React from 'react';

let Component = (props) => 
{
    let actualEndDate = () => {
        if(props.trip.actualEndDate !== "" 
            && props.trip.actualEndDate !== null 
            && props.trip.actualEndDate !== undefined){
            return <td>{props.trip.actualEndDate.slice(0, 10)}</td>;
        }
        return <td>-</td>;
    }

    let isBooked = () => {
        if(props.trip.isBooked !== "" 
            && props.trip.isBooked !== null 
            && props.trip.isBooked !== undefined
            && props.trip.isBooked === true){
            return <td><span className="text-success">Yes</span></td>;
        }
        return <td><span className="text-warning">No</span></td>;
    }
    
    return (
        <div className="form">
            <h3 className="form-header">Deleting Trip{" " + props.trip.tripID}</h3>
            <table className="table table-striped">
                <tbody>
                    <tr>
                        <th>Start Date</th>
                        <td>{props.trip.startDate.slice(0, 10)}</td>
                    </tr>
                    <tr>
                        <th>Projected End Date</th>
                        <td>{props.trip.projectedEndDate.slice(0, 10)}</td>
                    </tr>
                    <tr>
                        <th>Actual End Date</th>
                        {actualEndDate()}
                    </tr>
                    <tr>
                        <th>Booked?</th>
                        {isBooked()}
                    </tr>
                </tbody>
            </table>
            <form onSubmit={(event => {event.preventDefault(); props.handleDelete(props.trip);})}>
                <button className="btn btn-danger btn-submit" type="submit">Confirm Delete</button><br/>
                <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
            </form>
        </div>
    );
}
export default Component;