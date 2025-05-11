using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelUpUIManager : MonoBehaviour
{
    public GameObject levelUpUI;             // 레벨업 UI 패널
    public Button optionButton1;              // 첫 번째 선택지 버튼
    public Button optionButton2;              // 두 번째 선택지 버튼
    public TMP_Text optionText1;              // 첫 번째 선택지 텍스트
    public TMP_Text optionText2;              // 두 번째 선택지 텍스트

    private LevelUpOption option1;            // 선택지1 데이터
    private LevelUpOption option2;            // 선택지2 데이터

    public void Start()
    {
        levelUpUI.SetActive(false);            // 게임 시작 시 UI 비활성화
    }

    public void OpenLevelUpUI()
    {
        levelUpUI.SetActive(true);              // 레벨업 UI 활성화
        Time.timeScale = 0f;                    // 게임 정지

        // 플레이어 이동 불가 처리
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        

        GenerateOptions();                      // 선택지 생성
    }

    public void CloseLevelUpUI()
    {
        levelUpUI.SetActive(false);              // 레벨업 UI 비활성화
        Time.timeScale = 1f;                     // 게임 재개

        // 플레이어 이동 가능 복구
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void GenerateOptions()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        WeaponManager weaponManager = player.GetComponentInChildren<WeaponManager>();

        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager를 찾을 수 없습니다!");
            return;
        }

        // ----------------------------
        // (1) 무기 추가 모드
        // ----------------------------
        if (weaponManager.CanAddWeapon() && weaponManager.HasAvailableWeapons())
        {
            GameObject weaponPrefabA = weaponManager.GetRandomAvailableWeapon();
            GameObject weaponPrefabB = null;

            bool onlyOneChoice = !weaponManager.HasAvailableWeapons();

            if (!onlyOneChoice)
            {
                weaponPrefabB = weaponManager.GetRandomAvailableWeapon();
            }

            // 첫 번째 무기 선택지 설정
            option1 = new LevelUpOption(weaponPrefabA);
            optionText1.text = $"새 무기: {weaponPrefabA.name}";
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() => ChooseNewWeapon(option1));
            optionButton1.gameObject.SetActive(true);

            if (!onlyOneChoice && weaponPrefabB != null)
            {
                // 두 번째 무기 선택지 설정
                option2 = new LevelUpOption(weaponPrefabB);
                optionText2.text = $"새 무기: {weaponPrefabB.name}";
                optionButton2.onClick.RemoveAllListeners();
                optionButton2.onClick.AddListener(() => ChooseNewWeapon(option2));
                optionButton2.gameObject.SetActive(true);
                ResetButtonPositions(); // 버튼 원래 위치로
            }
            else
            {
                // 하나만 선택할 경우 버튼 가운데 정렬
                optionButton2.gameObject.SetActive(false);
                CenterButton(optionButton1);
            }
        }
        // ----------------------------
        // (2) 업그레이드 모드
        // ----------------------------
        else
        {
            List<WeaponBase> weaponList = weaponManager.equippedWeapons;

            if (weaponList.Count == 0)
            {
                Debug.LogWarning("장착한 무기가 없습니다!");
                return;
            }

            // 첫 번째 무기 + 업그레이드 타입 선택
            WeaponBase weaponA = weaponList[Random.Range(0, weaponList.Count)];
            UpgradeType upgradeA = (Random.value > 0.5f) ? UpgradeType.Damage : UpgradeType.Speed;
            option1 = new LevelUpOption(weaponA, upgradeA);

            // 두 번째 무기 + 업그레이드 타입 선택 (첫 번째와 완전 동일 조합은 피함)
            WeaponBase weaponB;
            UpgradeType upgradeB;
            int tryCount = 0;

            do
            {
                weaponB = weaponList[Random.Range(0, weaponList.Count)];
                upgradeB = (Random.value > 0.5f) ? UpgradeType.Damage : UpgradeType.Speed;
                tryCount++;
            }
            while (weaponB == weaponA && upgradeB == upgradeA && tryCount < 30);

            option2 = new LevelUpOption(weaponB, upgradeB);

            // 버튼 텍스트 설정
            optionText1.text = $"{weaponA.weaponName} {(upgradeA == UpgradeType.Damage ? "데미지 업" : "속도 업")}";
            optionText2.text = $"{weaponB.weaponName} {(upgradeB == UpgradeType.Damage ? "데미지 업" : "속도 업")}";

            // 버튼 이벤트 연결
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() => ChooseUpgrade(option1));

            optionButton2.onClick.RemoveAllListeners();
            optionButton2.onClick.AddListener(() => ChooseUpgrade(option2));

            optionButton1.gameObject.SetActive(true);
            optionButton2.gameObject.SetActive(true);
            ResetButtonPositions();
        }
    }

    private void ChooseNewWeapon(LevelUpOption selectedOption)
    {
        if (selectedOption == null)
        {
            Debug.LogError("선택된 새 무기 옵션이 없습니다!");
            return;
        }

        // 선택한 무기 장착
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        WeaponManager weaponManager = player.GetComponentInChildren<WeaponManager>();

        if (weaponManager != null)
        {
            weaponManager.AddWeapon(selectedOption.weaponPrefab);
            Debug.Log($"{selectedOption.weaponPrefab.name} 무기 추가 완료!");
        }

        CloseLevelUpUI(); // 선택 완료 후 UI 닫기
    }

    private void ChooseUpgrade(LevelUpOption selectedOption)
    {
        if (selectedOption == null)
        {
            Debug.LogError("선택된 업그레이드 옵션이 없습니다!");
            return;
        }

        // 선택한 업그레이드 타입 적용
        if (selectedOption.upgradeType == UpgradeType.Damage)
        {
            selectedOption.weapon.UpgradeDamage();
            Debug.Log($"{selectedOption.weapon.weaponName} 데미지 업그레이드!");
        }
        else if (selectedOption.upgradeType == UpgradeType.Speed)
        {
            selectedOption.weapon.UpgradeSpeed();
            Debug.Log($"{selectedOption.weapon.weaponName} 속도 업그레이드!");
        }

        CloseLevelUpUI(); // 선택 완료 후 UI 닫기
    }

    private void CenterButton(Button button)
    {
        // 버튼을 화면 중앙으로 이동
        RectTransform rect = button.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
    }

    private void ResetButtonPositions()
    {
        // 버튼 2개를 좌우로 배치
        RectTransform rect1 = optionButton1.GetComponent<RectTransform>();
        RectTransform rect2 = optionButton2.GetComponent<RectTransform>();

        rect1.anchoredPosition = new Vector2(-200, 0);
        rect2.anchoredPosition = new Vector2(200, 0);
    }
}
