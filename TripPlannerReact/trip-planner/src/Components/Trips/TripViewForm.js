import React from 'react';

let Component = (props) => 
{
    return (
        <div className="form">
            <h3 className="form-header">Viewing Agent{" " + props.agent.agentId}</h3>
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
            <form onSubmit={(event) => {event.preventDefault(); props.exitView();}}>
                <button className="btn btn-secondary btn-submit" type="submit">Stop Viewing</button>
            </form>
        </div>
    );
}
export default Component;