import React, { useState } from 'react';
import { Button, Form, Row, Col, Alert } from 'react-bootstrap';
import axios from 'axios';

function DatasetForm(props) {
  const [validated, setValidated] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [alerts, setAlerts] = useState([]);
  const submit = (e) => {
    setAlerts([]);
    if (e.currentTarget.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
      setValidated(true);
      return;
    }

    e.preventDefault();
    setValidated(false);
    setIsLoading(true);
    axios.post('Upload', new FormData(e.target))
    .then(result => {
      setIsLoading(false);
      props.onLoadModule();
    })
    .catch(result => {
      setIsLoading(false);
      if (!result.response.data.errors){
        if (typeof result.response.data === 'string'){
          setAlerts([result.response.data])
        }
        else {
          setAlerts(Object.values(result.response.data).flat(1));
        }
      } else {
        setAlerts(Object.values(result.response.data.errors).flat(1));
      }
      if (result.response.status === 500) {
        setAlerts(["Произошла ошибка на стороне сервера"])
      }
    });
  };

  return (
    <Form noValidate validated={validated} onSubmit={submit}>
      {alerts.map((alert, idx) => 
        <Alert key={idx} variant="danger">
          {alert}
        </Alert>)}
      <Row className="mb-3">
        <Form.Group className="mb-3" controlId="formDatasetName">
          <Form.Label>Имя</Form.Label>
          <Form.Control required type="text" name="name" placeholder="Введите имя набора данных" />
          <Form.Control.Feedback type="invalid">
            Укажите имя набора данных.
          </Form.Control.Feedback>
        </Form.Group>
      </Row>

      <Row className="mb-3">
        <Form.Group as={Col} className="mb-3" md="4" controlId="formIsCyrillicCheckbox">
          <Form.Check 
            name="isCyrillic"
            type="checkbox"
            id="isCyrillicCheckbox"
            label="Содержит кириллицу"
            value
          />
        </Form.Group>
        <Form.Group as={Col} className="mb-3" md="4" controlId="formIsLatinCheckbox">
          <Form.Check 
            name="isLatin"
            type="checkbox"
            id="isLatinCheckbox"
            label="Содержит латиницу"
            value
          />
        </Form.Group>
        <Form.Group as={Col} className="mb-3" md="4" controlId="formIsNumericCheckbox">
          <Form.Check 
            name="isNumeric"
            type="checkbox"
            id="isNumericCheckbox"
            label="Содержит цифры"
            value
          />
        </Form.Group>
      </Row>

      <Row className="mb-3">
        <Form.Group as={Col} className="mb-3" md="4" controlId="formIsSpecialSymbolsCheckbox">
          <Form.Check 
            name="isSpecialSymbols"
            type="checkbox"
            id="isSpecialSymbolsCheckbox"
            label="Содержит специальные символы"
            value
          />
        </Form.Group>
        <Form.Group as={Col} className="mb-3" md="4" controlId="formIsRegisterSpecificCheckbox">
          <Form.Check 
            name="isRegisterSpecific"
            type="checkbox"
            id="isRegisterSpecificCheckbox"
            label="Чувствительность к регистру"
            value
          />
        </Form.Group>
        <Form.Group as={Col} className="mb-3" md="4" controlId="formAnswersLocation">
          <Form.Label>Расположение ответов на картинки</Form.Label>
          <Form.Select name="answersLocation" aria-label="Default select example">
            <option value="0">Отсутствует</option>
            <option value="1">В именах файлов</option>
            <option value="2">В отдельном файле</option>
          </Form.Select>
        </Form.Group>
      </Row>

      <Form.Group className="mb-3" controlId="formFile">
        <Form.Label>Архив картинок</Form.Label>
        <Form.Control required name="file" type="file" />
        <Form.Control.Feedback type="invalid">
          Укажите архив с набором данных.
        </Form.Control.Feedback>
      </Form.Group>

      <Button disabled={isLoading} variant="primary" type="submit">
        Загрузить
      </Button>
    </Form>
  );
}

export default DatasetForm;