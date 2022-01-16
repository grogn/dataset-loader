import React, { useEffect, useState } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import DatasetForm from './DatasetForm';
import ModuleList from './ModuleList';
import axios from 'axios';

function App() {
  const [modules, setModules] = useState([]);
  useEffect(() => {
    updateModules();
  }, [])

  const updateModules = () => {
    axios.get('Modules')
    .then((response) => {
      setModules(response.data);
    })
  }
  return (
    <Container>
      <Row>
        <Col>
          <DatasetForm onLoadModule={updateModules}/>
        </Col>
        <Col>
          <ModuleList modules={modules}/>
        </Col>
      </Row>
    </Container>
  );
}

export default App;