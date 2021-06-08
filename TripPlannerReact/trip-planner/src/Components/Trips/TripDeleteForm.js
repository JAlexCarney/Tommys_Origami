import React from 'react';

let Component = (props) => 
{
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
                        <th>End Date</th>
                        <td>{props.trip.projectedEndDate.slice(0, 10)}</td>
                    </tr>
                </tbody>
            </table>
            <form onSubmit={(event => {event.preventDefault(); props.handleDelete(props.agent);})}>
                <button className="btn btn-danger btn-submit" type="submit">Confirm Delete</button><br/>
                <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
            </form>
        </div>
    );
}
export default Component;