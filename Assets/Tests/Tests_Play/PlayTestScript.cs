using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BoidsSimulationOnGPU;

namespace PlayTest
{
    public class PlayTestScript
    {
        GameObject gameObject;

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(gameObject);
        }

        [UnityTest]
        public IEnumerator CheckManagerProperties()
        {
            gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Simulation/Manager"));
            var manager = gameObject.GetComponent<SimulationManager>();
            yield return new WaitForSeconds(0.1f);
            // 현재 Github Action의 단위테스트에서 .compute파일을 읽어오지 못하는 문제가 있는듯하여 검사대상에서 제외
            var isPropertyCheck =
                //manager.GPUBoids != null &&
                manager.CameraOperate != null;
            Assert.IsTrue(isPropertyCheck, "Check Simulation Manager Properties...");
        }

        [UnityTest]
        public IEnumerator CameraMoveToObject()
        {
            gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Simulation/Manager"));
            yield return new WaitForSeconds(0.1f);

            var isMovedCheck = true;
            var mainCamera = gameObject.transform.Find("CameraJig/Main Camera");
            var tmpAnchor = gameObject.transform.Find("EventSystem");
            var cameraMove = mainCamera.GetComponent<CameraMoveToObject>();

            float preX = mainCamera.position.x;

            // 위치 랜덤 이동. EventSystem을 목표좌표로 지정
            tmpAnchor.position = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
            tmpAnchor.rotation = Quaternion.Euler(mainCamera.position);
            cameraMove.OnTrnasferButtonClick(tmpAnchor);
            yield return new WaitForSeconds(0.1f);
            if (Mathf.Abs(preX -= mainCamera.position.x) < 0.001f) isMovedCheck = false;
            Assert.IsTrue(isMovedCheck, "Check Move Camera Position when Click Button...");
        }

        [UnityTest]
        public IEnumerator StageYRotateAction()
        {
            gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Simulation/Manager"));
            yield return new WaitForSeconds(0.1f);

            var isRot     = false;
            var isRotStop = false;
            var mainCamera = gameObject.transform.Find("CameraJig");
            // 스피드 임의의 큰값 적용
            mainCamera.GetComponent<StageYRotate>().speed = 65535f;

            // Manager의 CameraJig는 생성시 AddRotAction() 함수를 실행함
            // AddRotAction
            var roty = mainCamera.rotation.y;
            yield return new WaitForSeconds(0.2f); 
            if (Mathf.Abs(roty -= mainCamera.rotation.y) > 0.01f) isRot = true;

            // RemoveRotAction
            mainCamera.GetComponent<StageYRotate>().RemoveRotAction();
            roty = mainCamera.rotation.y;
            yield return new WaitForSeconds(0.2f);
            if (Mathf.Abs(roty -= mainCamera.rotation.y) < 0.01f) isRotStop = true;

            Assert.IsTrue(isRot && isRotStop, "Check Add/Remove Stage Y Rot Action...");
        }

        [UnityTest]
        public IEnumerator CheckBtnsEventInGroupPanel()
        {
            gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Simulation/Canvas"));
            yield return new WaitForSeconds(0.2f);
            var isEventCheck = true;
            var group_panels = gameObject.transform.Find("Group_BottomBar/Group_panels");
            var btns = group_panels.GetComponentsInChildren<UnityEngine.UI.Button>();
            foreach(var btn in btns)
            {
                // 동적으로 추가된 delegate이벤트는 직접 count갯수를 받아오지 못하는듯함
                // 버튼에 이벤트가 제대로 추가된경우 interactable은 true로 set
                if (!btn.interactable) isEventCheck = false;
            }
            Assert.IsTrue(isEventCheck, "Check Button Event in Group Panel...");
        }

        [UnityTest]
        public IEnumerator OnClickPanelBtnsClick()
        {
            gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Simulation/Canvas"));
            yield return new WaitForSeconds(0.1f);

            var isMovedCheck = true;
            var group_panels = gameObject.transform.Find("Group_BottomBar/Group_panels");
            var botAnimMgr = gameObject.transform.Find("Group_BottomBar").GetComponent<BottomBarAnimation>();
            var panels = group_panels.GetComponentsInChildren<UnityEngine.UI.Text>();
            foreach (var panel in panels)
            {
                float preX = botAnimMgr.SelectedPos;
                botAnimMgr.OnPanelSelect(panel.transform);
                yield return new WaitForSeconds(0.1f);
                if (Mathf.Abs(preX -= botAnimMgr.SelectedPos) < 0.001f) isMovedCheck = false;
            }
            Assert.IsTrue(isMovedCheck, "Check Move Selected Position when Click Button...");
        }
    }
}
