import './App.css';
import TripPlanner from './Components/TripPlanner';

function App() {
  return (
    <div>
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark border-bottom box-shadow mb-3">
          <div className="container">
            <div className="row">
              <div className="col col-9">
                <span className="navbar-brand">Trip Planner</span>
              </div>
            </div>
          </div>
        </nav>
      </header>
      <div className="container">
        <TripPlanner />
      </div>
    </div>
  );
}

export default App;
