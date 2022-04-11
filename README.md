# matchingcolor-3dgame
색깔 맞추기 게임 제작

https://user-images.githubusercontent.com/90877724/159411053-7e9d8bb0-fb65-4dd4-8da9-d8990c8985d3.mp4

## 1. 프로젝트 개요
### 1.1 개발 인원/기간 및 포지션
- 1인, 총 2일 소요
### 1.2 개발 환경
- Unity 2020.3.16f
- 언어 : C#
- OS : Window 10
## 2. 핵심 구현 내용
### 2.1 이동 큐브셋 스폰 코루틴 함수 생성
- CubeSet 오브젝트의 자식으로 있는 9개 큐브의 MeshRenderer 컴포넌트 정보 호출
- CubeColors의 길이(2개)만큼 for문을 돌려 9개의 큐브가 Random 색상(빨강, 파랑)으로 스폰
```c#
private IEnumerator SpawnCubeSet()
    {
        while(true)
        {
            GameObject clone = Instantiate(cubesetPrefabs, spawnPoints.position, Quaternion.identity);
            MeshRenderer[] renderers = clone.GetComponentsInChildren<MeshRenderer>();

            for(int i = 0; i < renderers.Length; ++i)
            {
                int index = Random.Range(0, CubeColors.Length);
                renderers[i].material.color = CubeColors[index];
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
```
### 2.2 메인 큐브셋 색상 변경
- 색상 변경 함수 로직
```c#
public void ChangeColor()
    {
        if(colorIndex < cubeSpawner.CubeColors.Length-1)
        {
            colorIndex++;
        }
        else
        {
            colorIndex = 0;
        }

        meshRenderer.material.color = cubeSpawner.CubeColors[colorIndex];
    }
```
- Input.GetMouseButtonDown(0) 입력 시 mousePosition로부터 Raycast 정보 수집
- raycastEvent.AddListener(SelectCube) 함수로 raycastEvent 이벤트에 SelectCube 메소드 등록, ChangeColor() 함수 호출
### 2.3 이동 큐브셋과 메인 큐브셋 색상 일치 여부 판정
- 색상 일치 시 correctCount, 색상 불일치 시 incorrectCount 증가하는 프로퍼티 생성
- OnTriggerEnter 콜라이더 체크로 MeshRenderer.material.color 일치 여부 판정
```c#
private void OnTriggerEnter(Collider other)
    {
        MeshRenderer renderer = other.GetComponent<MeshRenderer>();

        if(meshRenderer.material.color == renderer.material.color)
        {
            cubeChecker.CorrectCount++;
        }
        else
        {
            cubeChecker.IncorrectCount++;
        }
    } 
```
### 2.4 점수 출력 UI 화면 구성
- 일치 시 gameController.IncreaseScore() 함수를 호출하여 점수 Text UI 증가
- 불일치 시 gameController의 GameOver() 함수 호출과 함께 게임 종료 Panel, 현재 점수 및 최고 점수 Text UI 활성화
- GameOver 시 점수 판정 기능 정지, Restart 버튼 추가
```c#
public void GameOver()
    {
        IsGameOver = true;
        textScore.enabled = false;
        panelResult.SetActive(true);

        int highScore = PlayerPrefs.GetInt("HighScore");

        if(score < highScore)
        {
            textHighScore.text = $"High Score { highScore}";
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);

            textHighScore.text = $"Hight Score {score}";
        }

        textResultScore.text = $"Score {score}";
    }
    
