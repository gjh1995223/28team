/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50623
Source Host           : localhost:3306
Source Database       : cantool

Target Server Type    : MYSQL
Target Server Version : 50623
File Encoding         : 65001

Date: 2017-10-23 14:26:57
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for can_message
-- ----------------------------
DROP TABLE IF EXISTS `can_message`;
CREATE TABLE `can_message` (
  `messageid` int(20) NOT NULL AUTO_INCREMENT,
  `messagetext` varchar(200) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`messageid`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of can_message
-- ----------------------------
INSERT INTO `can_message` VALUES ('1', 'BO_ 856 CDU_1: 8 CDU');
INSERT INTO `can_message` VALUES ('2', 'BO_ 61 CDU_4: 8 CDU');
INSERT INTO `can_message` VALUES ('3', 'BO_ 1067 CDU_NM: 8 CDU');
INSERT INTO `can_message` VALUES ('4', 'BO_ 1056 BCM_NM: 8 BCM');
INSERT INTO `can_message` VALUES ('5', 'BO_ 792 BCM_BCAN_1: 8 BCM');
INSERT INTO `can_message` VALUES ('6', 'BO_ 837 BCM_ESC_2: 8 BCM');
INSERT INTO `can_message` VALUES ('7', 'BO_ 915 BCM_VCU_2: 8 BCM');
INSERT INTO `can_message` VALUES ('8', 'BO_ 800 HVAC_1: 8 HVAC');
INSERT INTO `can_message` VALUES ('9', 'BO_ 801 HVAC_2: 8 HVAC');
INSERT INTO `can_message` VALUES ('10', 'BO_ 797 HVAC_3: 8 HVAC');
INSERT INTO `can_message` VALUES ('11', 'BO_ 864 HVAC_4: 8 ACP');
INSERT INTO `can_message` VALUES ('12', 'BO_ 867 ACP_1: 8 ACP');
INSERT INTO `can_message` VALUES ('13', 'BO_ 868 PTC_1: 8 PTC');

-- ----------------------------
-- Table structure for can_signal
-- ----------------------------
DROP TABLE IF EXISTS `can_signal`;
CREATE TABLE `can_signal` (
  `messageid` int(11) NOT NULL,
  `signaltext` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  KEY `mess_signal` (`messageid`),
  CONSTRAINT `mess_signal` FOREIGN KEY (`messageid`) REFERENCES `can_message` (`messageid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Records of can_signal
-- ----------------------------
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACOffButtonSt : 0|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACOffButtonStVD : 1|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACAutoModeButtonSt : 2|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACAutoModeButtonStVD : 3|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACFDefrostButtonSt : 6|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACFDefrostButtonStVD : 7|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACDualButtonSt : 10|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACDualButtonStVD : 11|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACIonButtonSt : 12|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACIonButtonStVD : 13|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACCirculationButtonSt : 18|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACCirculationButtonStVD : 19|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACACButtonSt : 20|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACACButtonStVD : 21|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACACMaxButtonSt : 22|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACACMaxButtonStVD : 23|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', ' SG_ CDU_HVACModeButtonSt : 26|3@0+ (1,0) [0|7] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', 'SG_ HVAC_WindExitSpd : 30|4@0+ (1,0) [0|15] \"\"   Vector__XXX');
INSERT INTO `can_signal` VALUES ('1', 'SG_ CDU_HVAC_DriverTempSelect : 36|5@0+ (0.5,18) [18|32] \"°C\"   Vector__XXX');
INSERT INTO `can_signal` VALUES ('1', ' SG_ HVAC_PsnTempSelect : 44|5@0+ (0.5,18) [18|32] \"\"   Vector__XXX');
INSERT INTO `can_signal` VALUES ('1', 'SG_ CDU_HVACCtrlModeSt : 54|3@0+ (1,0) [0|7] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('1', 'SG_ CDU_ControlSt : 55|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('2', 'SG_ CDU_HVACACCfg : 1|2@0+ (1,0) [0|3] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('2', ' SG_ CDU_HVACAirCirCfg : 3|2@0+ (1,0) [0|3] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('2', ' SG_ CDU_HVACComfortCfg : 5|2@0+ (1,0) [0|3] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('3', ' SG_ CDU_NMDestAddress : 7|8@0+ (1,0) [0|255] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('3', ' SG_ CDU_NMAlive : 8|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('3', ' SG_ CDU_NMRing : 9|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('3', 'SG_ CDU_NMLimpHome : 10|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('3', 'SG_ CDU_NMSleepInd : 12|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('3', ' SG_ CDU_NMSleepAck : 13|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('3', ' SG_ CDU_NMWakeupOrignin : 23|8@0+ (1,0) [0|255] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('3', 'SG_ CDU_NMDataField : 31|40@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', 'SG_ BCM_NMDestAddress : 7|8@0+ (1,0) [0|255] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', ' SG_ BCM_NMAlive : 8|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', ' SG_ BCM_NMRing : 9|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', ' SG_ BCM_NMLimpHome : 10|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', 'SG_ BCM_NMSleepInd : 12|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', ' SG_ BCM_NMSleepAck : 13|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', ' SG_ BCM_NMWakeupOrignin : 23|8@0+ (1,0) [0|255] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('4', ' SG_ BCM_NMDataField : 31|40@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,CDU');
INSERT INTO `can_signal` VALUES ('5', ' SG_ BCM_KeySt : 1|2@0+ (1,0) [1|3] \"\"  PEPS,ICM,AVM,CDU,HVAC');
INSERT INTO `can_signal` VALUES ('6', 'SG_ ESC_VehSpdVD : 37|1@0+ (1,0) [0|1] \"\"  BCM,PEPS,ICM,AVM,CDU');
INSERT INTO `can_signal` VALUES ('6', ' SG_ ESC_VehSpd : 36|13@0+ (0.05625,0) [0|240] \"\"  BCM,PEPS,ICM,AVM,CDU');
INSERT INTO `can_signal` VALUES ('7', ' SG_ VCU_CompressorPwrLimit : 21|6@0+ (100,0) [0|6000] \"w\"  HVAC');
INSERT INTO `can_signal` VALUES ('7', ' SG_ VCU_CompressorPwrLimitAct : 32|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('7', ' SG_ VCU_PTCPwrLimit : 29|6@0+ (100,0) [0|6000] \"w\"  HVAC');
INSERT INTO `can_signal` VALUES ('7', ' SG_ VCU_PTCrPwrLimitAct : 33|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('7', ' SG_ VCU_AirCompressorReq : 36|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('7', 'SG_ VCU_AirCompressorReqVD : 37|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('8', 'SG_ HVAC_AirCompressorSt : 2|3@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_CorrectedExterTempVD : 3|1@0+ (1,0) [0|1] \"\"  BCM,CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_RawExterTempVD : 4|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_EngIdleStopProhibitReq : 5|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_ACSt : 6|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_ACmaxSt : 7|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_CorrectedExterTemp : 15|8@0+ (0.5,-40) [-40|87.5] \"°C\"  BCM,CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_RawExterTemp : 23|8@0+ (0.5,-40) [-40|87.5] \"°C\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_TempSelect : 28|5@0+ (0.5,18) [18|32] \"°C\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_DualSt : 29|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_AutoSt : 30|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_Type : 31|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_WindExitMode : 34|3@0+ (1,0) [0|7] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_SpdFanReq : 36|2@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_TelematicsSt : 42|3@0+ (1,0) [0|7] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_AirCirculationSt : 46|2@0+ (1,0) [0|3] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_PopUpDisplayReq : 47|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_DriverTempSelect : 53|5@0+ (0.5,18) [18|32] \"°C\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_IonMode : 55|2@0+ (1,0) [0|3] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_WindExitSpd : 59|4@0+ (1,0) [0|15] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('8', ' SG_ HVAC_PsnTempSelect : 48|5@0+ (0.5,18) [18|32] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('9', ' SG_ HVAC_RawCabinTemp : 7|8@0+ (0.5,-40) [-40|87.5] \"°C\"  CDU');
INSERT INTO `can_signal` VALUES ('9', ' SG_ HVAC_CorrectedCabinTemp : 15|8@0+ (0.5,-40) [-40|87.5] \"°C\"  CDU');
INSERT INTO `can_signal` VALUES ('9', ' SG_ HVAC_RawCabinTempVD : 19|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('9', ' SG_ HVAC_CompressorComsumpPwr : 17|10@0+ (10,0) [0|8000] \"w\"  BCM');
INSERT INTO `can_signal` VALUES ('9', ' SG_ HVAC_PTCPwrAct : 33|10@0+ (10,0) [0|8000] \"w\"  BCM');
INSERT INTO `can_signal` VALUES ('9', 'SG_ HVAC_stPTCAct : 55|3@0+ (1,0) [0|1] \"\"  BCM');
INSERT INTO `can_signal` VALUES ('9', ' SG_ HVAC_CorrectedCabinTempVD : 18|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('10', ' SG_ HVAC_ACCfgSt : 0|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('10', ' SG_ HVAC_AirCirCfgSt : 1|1@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('10', ' SG_ HVAC_ComfortCfgSt : 3|2@0+ (1,0) [0|1] \"\"  CDU');
INSERT INTO `can_signal` VALUES ('11', 'SG_ HVAC_ACPCommandVD : 0|1@0+ (1,0) [0|1] \"\"  ACP');
INSERT INTO `can_signal` VALUES ('11', 'SG_ HVAC_ACPCommand : 2|2@0+ (1,0) [0|3] \"\"  ACP');
INSERT INTO `can_signal` VALUES ('11', ' SG_ HVAC_ACPSpeedSet : 14|7@0+ (100,0) [0|8600] \"\"  ACP');
INSERT INTO `can_signal` VALUES ('11', ' SG_ HVAC_ACPHighSidePress : 21|6@0+ (0.5,0) [0|31] \"\"  ACP');
INSERT INTO `can_signal` VALUES ('11', ' SG_ HVAC_PTCPowerRatio : 31|8@0+ (1,0) [0|100] \"\"  PTC');
INSERT INTO `can_signal` VALUES ('11', ' SG_ HVAC_Checksum : 39|8@0+ (1,0) [155|255] \"\"  PTC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACP_Speed : 6|7@0+ (100,0) [0|8600] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACPComsumpPwr : 15|10@0+ (10,0) [0|8000] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACP_Current : 16|9@0+ (0.1,0) [0|51] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACP_MotorTemp : 39|8@0+ (1,-40) [-40|140] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACP_HearBeat : 55|4@0+ (1,0) [0|15] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACP_ExtState : 58|3@0+ (1,0) [0|7] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACP_FailGrade : 60|2@0+ (1,0) [0|3] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('12', ' SG_ ACP_BaseState : 63|3@0+ (1,0) [0|7] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('13', ' SG_ PTC_ElementError : 7|4@0+ (1,0) [0|15] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('13', ' SG_ PTC_TemperatureOver : 3|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('13', ' SG_ PTC_VoltageFault : 2|1@0+ (1,0) [0|1] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('13', ' SG_ PTC_InternalError : 1|2@0+ (1,0) [0|3] \"\"  HVAC');
INSERT INTO `can_signal` VALUES ('13', ' SG_ PTC_Current : 15|8@0+ (0.2,0) [0|25.4] \"A\"  HVAC');
INSERT INTO `can_signal` VALUES ('13', ' SG_ PTCPwrAct : 23|10@0+ (10,0) [0|8000] \"w\"  HVAC');
INSERT INTO `can_signal` VALUES ('13', ' SG_ PTCActst : 26|3@0+ (1,0) [0|7] \"\"  HVAC');
